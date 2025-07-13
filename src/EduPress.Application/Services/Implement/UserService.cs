using AutoMapper;
using EduPress.Application.Helpers.GenerateJWT;
using EduPress.Application.Models;
using EduPress.Application.Models.User;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.Core.Enums;
using EduPress.DataAccess.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
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
    }
}
