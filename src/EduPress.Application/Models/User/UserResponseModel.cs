using EduPress.Core.Enums;

namespace EduPress.Application.Models.User
{
    public class UserResponseModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
