namespace EduPress.Application.Models.CourseFaqsModel
{
    public class CreateCourseFaqsModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid CoursesId { get; set; }
    }

    public class CreateCourseFaqsResponseModel : BaseResponseModel { }
}
