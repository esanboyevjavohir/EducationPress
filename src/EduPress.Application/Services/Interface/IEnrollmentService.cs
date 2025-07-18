using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.EnrollmentModel;

namespace EduPress.Application.Services.Interface
{
    public interface IEnrollmentService
    {
        Task<ApiResult<EnrollmentResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<EnrollmentResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateEnrollmentResponseModel>> CreateAsync(CreateEnrollmentModel create);
        Task<ApiResult<UpdateEnrollmentResponseModel>> UpdateAsync(Guid id, UpdateEnrollmentModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
