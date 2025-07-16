using EduPress.Core.Entities;
using EduPress.Core.Enums;

namespace EduPress.Application.Models.EnrollmentModel
{
    public class CreateEnrollmentModel
    {
        public Guid UserId { get; set; }
        public Guid CoursesId { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class CreateEnrollmentResponseModel : BaseResponseModel { }
}
