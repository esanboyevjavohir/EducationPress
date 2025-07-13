using EduPress.Core.Common;
using EduPress.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace EduPress.Core.Entities
{
    public class Courses : BaseEntity, IAuditedEntity
    {
        public string Title { get; set; }
        public bool IsFree { get; set; } 
        public int StudentCount { get; set; }
        public int DurationMonth { get; set; }
        public int TotalLessons { get; set; }
        public Level Level { get; set; }
        public Guid CategoriesId { get; set; }
        public Categories Categories { get; set; }
        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
        public ICollection<CourseSection> CourseSections { get; set; } = new List<CourseSection>();
        public ICollection<CourseFaqs> CourseFaqs { get; set; } = new List<CourseFaqs>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
