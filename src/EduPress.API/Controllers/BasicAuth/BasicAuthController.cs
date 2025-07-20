using EduPress.Application.Helpers.BasicAuth.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduPress.API.Controllers.BasicAuth
{
    public class BasicAuthController : ApiController
    {
        [HttpPost("token"), BasicAuthorization]
        public IActionResult Token()
        {
            var userName = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)?
                .Value;

            var response = new
            {
                Message = "Auth is up!",
                ServerTime = DateTime.Now,
                Username = userName
            };

            return Ok(response);
        }
    }
}
