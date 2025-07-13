using EduPress.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPress.Core.Entities
{
    public class CourseSection : BaseEntity, IAuditedEntity
    {
        public string Title { get; set; }
        public int TotalLessons { get; set; }
        public int DurationMinutes { get; set; }
        public Guid CoursesId { get; set; }
        public Courses Courses { get; set; }
        public ICollection<CourseLessons> CourseLessons { get; set; } = new List<CourseLessons>();

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
