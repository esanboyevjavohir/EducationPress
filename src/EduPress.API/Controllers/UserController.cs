using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduPress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Login")]
        public async Task<string> LoginAsync()
        {
            return "Test";
        }
    }
}
