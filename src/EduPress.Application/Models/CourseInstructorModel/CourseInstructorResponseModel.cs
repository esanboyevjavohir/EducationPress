using EduPress.Core.Entities;

namespace EduPress.Application.Models.CourseInstructorModel
{
    public class CourseInstructorResponseModel : BaseResponseModel
    {
        public Guid CoursesId { get; set; }
        public Courses Courses { get; set; }
        public Guid InstructorsId { get; set; }
        public Instructors Instructors { get; set; }
    }
}
