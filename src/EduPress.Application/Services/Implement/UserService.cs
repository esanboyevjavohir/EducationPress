using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Helpers;
using EduPress.Application.Helpers.GenerateJWT;
using EduPress.Application.Models;
using EduPress.Application.Models.User;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.Core.Enums;
using EduPress.DataAccess.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EduPress.Application.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly DatabaseContext _databaseContext;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly UserSettings _userSettings;
        private readonly IValidator<CreateUserModel> _createUserValidator;
        private readonly IValidator<ResetPasswordModel> _resetPasswordValidator;

        public UserService(IConfiguration configuration,
            IMapper mapper, 
            ILogger<UserService> logger, 
            DatabaseContext databaseContext, 
            IJwtTokenHandler jwtTokenHandler, 
            IPasswordHasher passwordHasher, 
            IEmailService emailService, 
            IOptions<UserSettings> userSettings,
            IValidator<CreateUserModel> createUserValidator, 
            IValidator<ResetPasswordModel> resetPasswordValidator)
        {
            _mapper = mapper;
            _logger = logger;
            _databaseContext = databaseContext;
            _jwtTokenHandler = jwtTokenHandler;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _userSettings = userSettings.Value;
            _createUserValidator = createUserValidator;
            _resetPasswordValidator = resetPasswordValidator;
        }

        public async Task<ApiResult<CreateUserResponseModel>> SignUpAsync(CreateUserModel createUserModel)
        {
            var validationResult = await _createUserValidator.ValidateAsync(createUserModel);

            if(!validationResult.IsValid)
            {
                return ApiResult<CreateUserResponseModel>
                        .Failure(validationResult.Errors
                            .Select(a => a.ErrorMessage));
            }

            var user = _mapper.Map<User>(createUserModel);

            string randomSalt = Guid.NewGuid().ToString();

            user.Role = UserRole.Student;
            user.Salt = randomSalt;
            user.PasswordHash = _passwordHasher.Encrypt(createUserModel.Password, randomSalt);
            user.CreatedOn = DateTime.Now;

            using var transaction = await _databaseContext.Database.BeginTransactionAsync();

            try
            {
                await _databaseContext.Users.AddAsync(user);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message, "Error occurred while creating user");
                await transaction.RollbackAsync();
                return ApiResult<CreateUserResponseModel>.Failure(errors: new List<string> { ex.InnerException?.Message });
            }

            await transaction.CommitAsync();

            return ApiResult<CreateUserResponseModel>.Success(new CreateUserResponseModel
            {
                Id = user.Id
            });
        }

        public async Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel loginModel)
        {
            var user = await _databaseContext.Users
                .FirstOrDefaultAsync(u => u.Email == loginModel.Email);

            if(user == null)
            {
                return ApiResult<LoginResponseModel>.Failure(new List<string> { "User not found" });
            }

            var hashedPassword = _passwordHasher.Encrypt(loginModel.Password, user.Salt);
            if(user.PasswordHash != hashedPassword)
            {
                return ApiResult<LoginResponseModel>.Failure(new List<string> { "Invalid password" });
            }

            var accessToken = _jwtTokenHandler.GenerateAccessToken(user);
            var refreshToken = _jwtTokenHandler.GenerateRefreshToken();

            user.CreatedOn = DateTime.Now;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = DateTime.Now.AddDays(_userSettings.RefreshTokenExpirationDays);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<LoginResponseModel>.Success(new LoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        } 

        public async Task<ApiResult<bool>> SendOtpCode(Guid userId)
        {
            var maybeUser = await _databaseContext.Users
                .Include(a => a.OtpCodes)
                .FirstOrDefaultAsync(a => a.Id == userId);

            if(maybeUser == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "User not found" });
            }

            var optCode = new OtpCode
            {
                Code = OtpCodeHelper.GenerateOtpCode(),
                Status = OtpCodeStatus.Unverified
            };

            maybeUser.OtpCodes.Add(optCode);

            bool isSent = await _emailService.SendEmailAsync(maybeUser.Email, optCode.Code);

            if(!isSent)
            {
                return ApiResult<bool>.Failure(new List<string> { "Failed to send OTP email" });
            }

            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<bool>> ResendOtpCode(Guid userId)
        {
            var user = await _databaseContext.Users
                .Include(a => a.OtpCodes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "User not found" });
            }

            var lastOtp = user.OtpCodes
                .OrderByDescending(otp => otp.CreatedAt)
                .FirstOrDefault();

            if (lastOtp == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "No OTP found to resend" });
            }

            if (!CanResend(lastOtp.CreatedAt))
            {
                var waitTimeSeconds = GetWaitTimeForResend(lastOtp.CreatedAt);
                return ApiResult<bool>.Failure(new List<string>
                { $"Please wait {waitTimeSeconds} seconds before requesting a new code" });
            }

            if(!IsExpired(lastOtp.CreatedAt))
            {
                bool isSent = await _emailService.SendEmailAsync(user.Email, lastOtp.Code);
                if (!isSent)
                {
                    return ApiResult<bool>.Failure(new List<string> { "Failed to send OTP email" });
                }
                return ApiResult<bool>.Success(true);
            }

            var newOtpCode = new OtpCode
            {
                Code = OtpCodeHelper.GenerateOtpCode(),
                Status = OtpCodeStatus.Unverified
            };

            user.OtpCodes.Add(newOtpCode);

            bool isSentNew = await _emailService.SendEmailAsync(user.Email, newOtpCode.Code);
            if (!isSentNew)
            {
                return ApiResult<bool>.Failure(new List<string> { "Failed to send OTP email" });
            }

            await _databaseContext.SaveChangesAsync();
            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<bool>> VerifyOtpCode(string code, Guid userId)
        {
            if(string.IsNullOrEmpty(code))
            {
                return ApiResult<bool>.Failure(new List<string> { "OTP code cannot be empty" });
            }

            var user = await _databaseContext.Users
                .Include(c => c.OtpCodes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "User not found" });
            }

            var lastOtp = user.OtpCodes
                .Where(o => o.Status == OtpCodeStatus.Unverified)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();

            if (lastOtp == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "No active OTP found" });
            }

            if (IsExpired(lastOtp.CreatedAt))
            {
                lastOtp.Status = OtpCodeStatus.Expired;
                await _databaseContext.SaveChangesAsync();
                return ApiResult<bool>.Failure(new List<string> { "OTP has expired" });
            }

            if (lastOtp.Code != code)
            {
                return ApiResult<bool>.Failure(new List<string> { "Invalid OTP code" });
            }

            lastOtp.Status = OtpCodeStatus.Verified;
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<TokenResponseModel>> ValidateAndRefreshToken(Guid id, string refreshToken)
        {
            var user = await _databaseContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return ApiResult<TokenResponseModel>.Failure(new List<string> { "User not found" });
            }

            if (user.RefreshToken != refreshToken)
            {
                return ApiResult<TokenResponseModel>.Failure(new List<string> { "Invalid refresh token" });
            }

            if (user.RefreshTokenExpireDate < DateTime.Now)
            {
                return ApiResult<TokenResponseModel>.Failure(new List<string> { "Unauthorized" });
            }

            var newRefreshToken = _jwtTokenHandler.GenerateRefreshToken();
            var newAccessToken = _jwtTokenHandler.GenerateAccessToken(user, newRefreshToken);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = DateTime.Now.AddDays(_userSettings.RefreshTokenExpirationDays);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<TokenResponseModel>.Success(new TokenResponseModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        public async Task<ApiResult<bool>> ForgotPasswordAsync(string email)
        {
            var user = await _databaseContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "User not found" });
            }

            string tempPassword = GenerateTemporaryPassword();
            user.ResetPasswordToken = _passwordHasher.Encrypt(tempPassword, user.Salt);
            user.ResetPasswordTokenExpiry = DateTime.Now.AddMinutes(10);

            await _databaseContext.SaveChangesAsync();

            bool emailSent = await _emailService.SendEmailAsync(user.Email, $"Your temporary password:" +
                $" {tempPassword}");

            return emailSent ? ApiResult<bool>.Success(true) : ApiResult<bool>.Failure(new List<string>
                { "Failed to send email" });
        }

        public async Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordModel model)
        {
            var validationResult = await _resetPasswordValidator.ValidateAsync(model);

            if(!validationResult.IsValid)
            {
                return ApiResult<bool>
                    .Failure(validationResult.Errors
                        .Select(a => a.ErrorMessage));
            }

            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "User not found" });
            }

            if (user.ResetPasswordTokenExpiry < DateTime.UtcNow)
            {
                return ApiResult<bool>.Failure(new List<string> { "Temporary password expired" });
            }

            if (user.ResetPasswordToken != _passwordHasher.Encrypt(model.TemporaryPassword, user.Salt))
            {
                return ApiResult<bool>.Failure(new List<string> { "Invalid temporary password" });
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return ApiResult<bool>.Failure(new List<string> { "Passwords do not match" });
            }

            user.PasswordHash = _passwordHasher.Encrypt(model.NewPassword, user.Salt);
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiry = null;

            await _databaseContext.SaveChangesAsync();
            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<UserResponseModel>> GetByIdAsync(Guid id)
        {
            var user = await _databaseContext.Users
                .AsNoTracking()
                .ProjectTo<UserResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null)
            {
                return ApiResult<UserResponseModel>.Failure(new List<string> { "User not found" });
            }

            return ApiResult<UserResponseModel>.Success(user);
        }

        public async Task<ApiResult<List<UserResponseModel>>> GetAllAsync()
        {
            var users = await _databaseContext.Users
                .AsNoTracking()
                .ProjectTo<UserResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<UserResponseModel>>.Success(users);
        }

        public async Task<ApiResult<UserResponseModel>> GetUserByEmailAsync(string email)
        {
            var user = await _databaseContext.Users
                .AsNoTracking()
                .ProjectTo<UserResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return ApiResult<UserResponseModel>.Failure(new List<string> { "User not found" });
            }

            return ApiResult<UserResponseModel>.Success(user);
        }

        public async Task<ApiResult<bool>> DeleteUserAsync(Guid id)
        {
            var user = await _databaseContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "User not found" });
            }

            _databaseContext.Users.Remove(user);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        private string GenerateTemporaryPassword()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        public bool IsExpired(DateTimeOffset createdAt) =>
            createdAt.AddSeconds(_userSettings.OtpExpirationTimeInSeconds) < DateTimeOffset.Now;

        private bool CanResend(DateTimeOffset createdAt) =>
            createdAt.AddSeconds(
                _userSettings.OtpExpirationTimeInSeconds - _userSettings.OtpResendTimeInSeconds) < DateTimeOffset.Now;

        private int GetWaitTimeForResend(DateTimeOffset createdAt)
        {
            var resendTime = createdAt.AddSeconds(
                _userSettings.OtpExpirationTimeInSeconds - _userSettings.OtpResendTimeInSeconds);
            var waitTime = resendTime - DateTimeOffset.Now;
            return Math.Max(0, (int)waitTime.TotalSeconds);
        }
    }
}
