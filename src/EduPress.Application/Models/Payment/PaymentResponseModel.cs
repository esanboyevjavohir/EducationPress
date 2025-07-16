using EduPress.Core.Enums;

namespace EduPress.Application.Models.Payment
{
    public class PaymentResponseModel : BaseResponseModel
    {
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public PayStatus PayStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid EnrollmentId { get; set; }
    }
}
