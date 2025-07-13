using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class Review : BaseEntity, IAuditedEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public bool IsVerified { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CoursesId { get; set; }
        public Courses Courses { get; set; }
        public ICollection<ReviewReplies> ReviewReplies { get; set; } = new List<ReviewReplies>();

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
