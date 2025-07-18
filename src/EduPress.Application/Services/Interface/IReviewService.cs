using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.ReviewModel;

namespace EduPress.Application.Services.Interface
{
    public interface IReviewService
    {
        Task<ApiResult<ReviewResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<ReviewResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateReviewResponseModel>> CreateAsync(CreateReviewModel create);
        Task<ApiResult<UpdateReviewResponseModel>> UpdateAsync(Guid id, UpdateReviewModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
