using AspNetCore.Authentication.Basic;
using Microsoft.AspNetCore.Authorization;

namespace EduPress.Application.Helpers.BasicAuth.Attributes
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme;
        }
    }
}
