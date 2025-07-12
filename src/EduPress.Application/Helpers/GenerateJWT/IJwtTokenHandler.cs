using EduPress.Core.Entities;

namespace EduPress.Application.Helpers.GenerateJWT
{
    public interface IJwtTokenHandler
    {
        string GenerateAccessToken(User user);
        string GenerateAccessToken(User user, string sessionToken);
        string GenerateRefreshToken(); 
    }
}
