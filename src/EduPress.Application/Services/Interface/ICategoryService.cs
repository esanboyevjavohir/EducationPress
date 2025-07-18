using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;

namespace EduPress.Application.Services.Interface
{
    public interface ICategoryService
    {
        Task<ApiResult<CategoriesResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<CategoriesResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateCategoriesResponseModel>> CreateAsync(CreateCategoriesModel create);
        Task<ApiResult<UpdateCategoriesResponseModel>> UpdateAsync(UpdateCategoriesModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
