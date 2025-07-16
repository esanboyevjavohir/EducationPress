namespace EduPress.Application.Models.InstructorsModel
{
    public class InstructorsResponseModel : BaseResponseModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int CourseCount { get; set; }
    }
}
