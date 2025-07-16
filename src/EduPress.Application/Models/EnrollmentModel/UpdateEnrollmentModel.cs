using EduPress.Core.Enums;

namespace EduPress.Application.Models.EnrollmentModel
{
    public class UpdateEnrollmentModel
    {
        public Guid UserId { get; set; }
        public Guid CoursesId { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class UpdateEnrollmentResponseModel : BaseResponseModel { }
}
