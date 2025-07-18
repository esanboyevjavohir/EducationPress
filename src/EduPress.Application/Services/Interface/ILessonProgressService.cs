using EduPress.Application.Models;
using EduPress.Application.Models.LessonProgressModel;

namespace EduPress.Application.Services.Interface
{
    public interface ILessonProgressService
    {
        Task<ApiResult<LessonProgressResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<LessonProgressResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateLessonProgressResponseModel>> CreateAsync(CreateLessonProgressModel create);
        Task<ApiResult<UpdateLessonProgressResponseModel>> UpdateAsync(Guid id, 
            UpdateLessonProgressModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
