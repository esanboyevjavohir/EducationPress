using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.CoursesModel;

namespace EduPress.Application.Services.Interface
{
    public interface ICoursesService
    {
        Task<ApiResult<CoursesResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<CoursesResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateCoursesResponseModel>> CreateAsync(CreateCoursesModel create);
        Task<ApiResult<UpdateCoursesResponseModel>> UpdateAsync(Guid id, UpdateCoursesModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
