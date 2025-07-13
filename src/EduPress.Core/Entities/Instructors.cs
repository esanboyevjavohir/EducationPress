using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class Instructors : BaseEntity, IAuditedEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int CourseCount { get; set; }
        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
