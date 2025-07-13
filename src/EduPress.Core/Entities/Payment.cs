using EduPress.Core.Common;
using EduPress.Core.Enums;

namespace EduPress.Core.Entities
{
    public class Payment : BaseEntity, IAuditedEntity
    {
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PayStatus PayStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
