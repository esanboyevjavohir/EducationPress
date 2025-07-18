using EduPress.Application.Models.User;
using EduPress.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduPress.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Registration")]
        public async Task<ActionResult<CreateUserResponseModel>> UserSignUpAsync(
            [FromForm] CreateUserModel createUserModel)
        {
            var create = await _userService.SignUpAsync(createUserModel);
            if (!create.Succedded)
                return BadRequest(create);

            return Created("", create);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> UserLoginAsync(LoginUserModel loginUser)
        {
            var result = await _userService.LoginAsync(loginUser);

            if (!result.Succedded)
            {
                if (result.Errors.Contains("User not found"))
                    return NotFound(result);
                if (result.Errors.Contains("Invalid password"))
                    return Unauthorized(result);

                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("ValidateAndRefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(Guid id, string refreshToken)
        {
            var result = await _userService.ValidateAndRefreshToken(id, refreshToken);
            return Ok(result);
        }

        [HttpPost("SendOtpCode")]
        public async Task<IActionResult> SendOtpCodeAsync(Guid userId)
        {
            var result = await _userService.SendOtpCode(userId);

            if (!result.Succedded)
            {
                if (result.Errors.Contains("User not found"))
                    return NotFound(result);

                if (result.Errors.Contains("Failed to send OTP email"))
                    return StatusCode(500, result);

                return BadRequest(result);
            }

            return Ok(result);
        }


    }
}
