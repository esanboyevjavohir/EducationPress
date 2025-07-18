using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.ReviewRepliesModel;

namespace EduPress.Application.Services.Interface
{
    public interface IReviewRepliesService
    {
        Task<ApiResult<ReviewRepliesResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<ReviewRepliesResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateReviewRepliesResponseModel>> CreateAsync(
            CreateReviewRepliesModel create);
        Task<ApiResult<UpdateReviewRepliesResponseModel>> UpdateAsync(Guid id, 
            UpdateReviewRepliesModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
