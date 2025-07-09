using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class CourseFaqs : BaseEntity, IAuditedEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid CoursesId { get; set; }
        public Courses Courses { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
