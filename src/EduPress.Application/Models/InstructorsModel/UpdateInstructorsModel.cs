namespace EduPress.Application.Models.InstructorsModel
{
    public class UpdateInstructorsModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int CourseCount { get; set; }
    }

    public class UpdateInstructorsResponseModel : BaseResponseModel { }
}
