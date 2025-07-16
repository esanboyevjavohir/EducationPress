namespace EduPress.Application.Models.ReviewModel
{
    public class ReviewResponseModel : BaseResponseModel
    { 
        public int Rating { get; set; }
        public string Comment { get; set; }
        public bool IsVerified { get; set; }
        public Guid UserId { get; set; }
        public Guid CoursesId { get; set; }
    }
}
