namespace EduPress.Application.Models.User
{
    public class CreateUserModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
    }

    public class CreateUserResponseModel : BaseResponseModel { }
}
