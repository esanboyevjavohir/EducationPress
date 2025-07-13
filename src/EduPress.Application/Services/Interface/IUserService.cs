using EduPress.Application.Models;
using EduPress.Application.Models.User;

namespace EduPress.Application.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResult<UserResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<UserResponseModel>>> GetAllAsync();
        Task<ApiResult<UserResponseModel>> GetUserByEmailAsync(string email);
        Task<ApiResult<CreateUserResponseModel>> SignUpAsync(CreateUserModel createUserModel);
        Task<ApiResult<bool>> SendOtpCode(Guid userId);
        Task<ApiResult<bool>> ResendOtpCode(Guid userId);
        Task<ApiResult<bool>> VerifyOtpCode(string code, Guid userId);  
        Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel loginModel);
        Task<ApiResult<string>> ValidateAndRefreshToken(Guid id, string refreshToken);
        Task<ApiResult<bool>> ForgotPasswordAsync(string email);
        Task<ApiResult<bool>> ResetPasswordAsync(ResetPasswordModel model);
        Task<ApiResult<bool>> DeleteUserAsync(Guid id);
    }
}
