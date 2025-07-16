using EduPress.Core.Entities;

namespace EduPress.Application.Models.CourseInstructorModel
{
    public class UpdateCourseInstructorModel
    {
        public Guid CoursesId { get; set; }
        public Guid InstructorsId { get; set; }
    }

    public class UpdateCourseInstructorResponseModel : BaseResponseModel { }
}
