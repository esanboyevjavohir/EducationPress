using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class CourseLessons : BaseEntity, IAuditedEntity
    {
        public string Title { get; set; }
        public TimeSpan DurationMinutes { get; set; }
        public bool IsFree { get; set; }
        public Guid CourseSectionId { get; set; }
        public CourseSection CourseSection { get; set; }
        public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
