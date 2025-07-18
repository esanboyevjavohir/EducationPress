using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.CourseSectionModel;

namespace EduPress.Application.Services.Interface
{
    public interface ICourseSectionService
    {
        Task<ApiResult<CourseSectionResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<CourseSectionResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateCourseSectionResponseModel>> CreateAsync(CreateCourseSectionModel create);
        Task<ApiResult<UpdateCourseSectionResponseModel>> UpdateAsync(Guid id, UpdateCourseSectionModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
