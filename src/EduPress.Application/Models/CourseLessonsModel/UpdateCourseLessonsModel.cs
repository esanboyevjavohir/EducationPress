namespace EduPress.Application.Models.CourseLessonsModel
{
    public class UpdateCourseLessonsModel
    {
        public string Title { get; set; }
        public TimeSpan DurationMinutes { get; set; }
        public bool IsFree { get; set; }
        public Guid CourseSectionId { get; set; }
    }
    
    public class UpdateCourseLessonsResponseModel : BaseResponseModel { }
}
