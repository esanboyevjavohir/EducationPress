using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.CourseLessonsModel;

namespace EduPress.Application.Services.Interface
{
    public interface ICourseLessonsService
    {
        Task<ApiResult<CourseLessonsResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<CourseLessonsResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateCourseLessonsResponseModel>> CreateAsync(CreateCourseLessonsModel create);
        Task<ApiResult<UpdateCourseLessonsResponseModel>> UpdateAsync(Guid id,
            UpdateCourseLessonsModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
