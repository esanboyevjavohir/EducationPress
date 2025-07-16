using EduPress.Core.Entities;

namespace EduPress.Application.Models.CourseInstructorModel
{
    public class CreateCourseInstructorModel
    {
        public Guid CoursesId { get; set; }
        public Guid InstructorsId { get; set; }
    }

    public class CreateCourseInstructorResponseModel : BaseResponseModel { }
}
