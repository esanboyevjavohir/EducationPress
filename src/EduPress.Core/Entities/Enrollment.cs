using EduPress.Core.Common;
using EduPress.Core.Enums;

namespace EduPress.Core.Entities
{
    public class Enrollment : BaseEntity, IAuditedEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CoursesId { get; set; }
        public Courses Courses { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public List<Payment> Payments = new List<Payment>();

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
