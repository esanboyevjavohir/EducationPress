using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.InstructorsModel;

namespace EduPress.Application.Services.Interface
{
    public interface IInstructorsService
    {
        Task<ApiResult<InstructorsResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<InstructorsResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateInstructorsResponseModel>> CreateAsync(CreateInstructorsModel create);
        Task<ApiResult<UpdateInstructorsResponseModel>> UpdateAsync(Guid id, UpdateInstructorsModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
