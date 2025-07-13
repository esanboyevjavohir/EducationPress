using EduPress.Core.Common;
using EduPress.Core.Enums;

namespace EduPress.Core.Entities
{
    public class User : BaseEntity, IAuditedEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = false;
        public string Salt { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }

        public ICollection<OtpCode> OtpCodes { get; set; } = new List<OtpCode>();

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
