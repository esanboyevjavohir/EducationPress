using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.Payment;

namespace EduPress.Application.Services.Interface
{
    public interface IPaymentService
    {
        Task<ApiResult<PaymentResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<PaymentResponseModel>>> GetAllAsync();
        Task<ApiResult<CreatePaymentResponseModel>> CreateAsync(CreatePaymentModel create);
        Task<ApiResult<UpdatePaymentResponseModel>> UpdateAsync(Guid id, 
            UpdatePaymentModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
