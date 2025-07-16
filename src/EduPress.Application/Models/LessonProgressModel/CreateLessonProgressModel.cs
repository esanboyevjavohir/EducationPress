using EduPress.Core.Entities;

namespace EduPress.Application.Models.LessonProgressModel
{
    public class CreateLessonProgressModel
    {
        public decimal CompletionPercentage { get; set; }
        public int LastPositionSeconds { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid CoursesId { get; set; }
        public Guid CourseLessonsId { get; set; }
    }

    public class CreateLessonProgressResponseModel : BaseResponseModel { }
}
