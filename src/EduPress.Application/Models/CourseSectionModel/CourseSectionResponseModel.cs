namespace EduPress.Application.Models.CourseSectionModel
{
    public class CourseSectionResponseModel : BaseResponseModel
    {
        public string Title { get; set; }
        public int TotalLessons { get; set; }
        public int DurationMinutes { get; set; }
        public Guid CoursesId { get; set; }
    }
}
