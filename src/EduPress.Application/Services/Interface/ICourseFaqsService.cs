using EduPress.Application.Models;
using EduPress.Application.Models.CourseFaqsModel;

namespace EduPress.Application.Services.Interface
{
    public interface ICourseFaqsService
    {
        Task<ApiResult<CourseFaqsResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<CourseFaqsResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateCourseFaqsResponseModel>> CreateAsync(CreateCourseFaqsModel create);
        Task<ApiResult<UpdateCourseFaqsResponseModel>> UpdateAsync(Guid id, UpdateCourseFaqsModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
