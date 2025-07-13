using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class CourseInstructor : BaseEntity, IAuditedEntity
    {
        public Guid CoursesId { get; set; }
        public Courses Courses { get; set; }
        public Guid InstructorsId { get; set; }
        public Instructors Instructors { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
