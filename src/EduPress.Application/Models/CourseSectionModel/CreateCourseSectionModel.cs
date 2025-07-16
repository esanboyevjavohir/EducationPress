namespace EduPress.Application.Models.CourseSectionModel
{
    public class CreateCourseSectionModel
    {
        public string Title { get; set; }
        public int TotalLessons { get; set; }
        public int DurationMinutes { get; set; }
        public Guid CoursesId { get; set; }
    }

    public class CreateCourseSectionResponseModel : BaseResponseModel { }
}
