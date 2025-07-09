using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class Categories : BaseEntity, IAuditedEntity
    {
        public string Name { get; set; }
        public int CourseCount { get; set; }
        public ICollection<Courses> Courses { get; set; } = new List<Courses>();

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
