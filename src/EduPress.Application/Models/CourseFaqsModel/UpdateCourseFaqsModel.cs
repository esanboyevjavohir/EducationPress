namespace EduPress.Application.Models.CourseFaqsModel
{
    public class UpdateCourseFaqsModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid CoursesId { get; set; }
    }

    public class UpdateCourseFaqsResponseModel : BaseResponseModel { }
}
