using EduPress.Core.Enums;

namespace EduPress.Application.Models.CoursesModel
{
    public class CreateCoursesModel
    {
        public string Title { get; set; }
        public bool IsFree { get; set; }
        public int StudentCount { get; set; }
        public int DurationMonth { get; set; }
        public int TotalLessons { get; set; }
        public Level Level { get; set; }
        public Guid CategoriesId { get; set; }
    }

    public class CreateCoursesResponseModel : BaseResponseModel { }
}
