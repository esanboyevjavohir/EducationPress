using EduPress.Application.Models;
using EduPress.Application.Models.ContactLocationsModel;

namespace EduPress.Application.Services.Interface
{
    public interface IContactLocationsService
    {
        Task<ApiResult<LocationsResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<LocationsResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateLocationsResponseModel>> CreateAsync(CreateLocationsModel create);
        Task<ApiResult<UpdateLocationsResponseModel>> UpdateAsync(Guid id, UpdateLocationsModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);  
    }
}
