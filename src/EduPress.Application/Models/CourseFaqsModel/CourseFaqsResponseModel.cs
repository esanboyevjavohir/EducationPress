namespace EduPress.Application.Models.CourseFaqsModel
{
    public class CourseFaqsResponseModel : BaseResponseModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid CoursesId { get; set; }
    }   
}
