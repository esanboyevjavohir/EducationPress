using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class ReviewReplies : BaseEntity, IAuditedEntity
    {
        public string ReplyText { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ReviewId { get; set; }
        public Review Review { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
