using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class LessonProgress : BaseEntity, IAuditedEntity
    {
        public decimal CompletionPercentage { get; set; }
        public int LastPositionSeconds { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CoursesId { get; set; }
        public Courses Courses { get; set; }
        public Guid CourseLessonsId { get; set; }
        public CourseLessons CourseLessons { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
