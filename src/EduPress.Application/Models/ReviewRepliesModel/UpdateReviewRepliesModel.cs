using EduPress.Core.Entities;

namespace EduPress.Application.Models.ReviewRepliesModel
{
    public class UpdateReviewRepliesModel
    {
        public string ReplyText { get; set; }
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
    }

    public class UpdateReviewRepliesResponseModel : BaseResponseModel { }
}
