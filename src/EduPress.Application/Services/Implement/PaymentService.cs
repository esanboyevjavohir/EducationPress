using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Models.Payment;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public PaymentService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreatePaymentResponseModel>> CreateAsync(CreatePaymentModel create)
        {
            var createModel = _mapper.Map<Payment>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.Payments.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreatePaymentResponseModel>.Success(new CreatePaymentResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.Payments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "Payment not found" });
            }

            _databaseContext.Payments.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<PaymentResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.Payments
                   .AsNoTracking()
                   .ProjectTo<PaymentResponseModel>(_mapper.ConfigurationProvider)
                   .ToListAsync();

            return ApiResult<List<PaymentResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<PaymentResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.Payments
                .AsNoTracking()
                .ProjectTo<PaymentResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<PaymentResponseModel>.Failure(
                                new List<string> { "Payment not found" });
            }

            return ApiResult<PaymentResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdatePaymentResponseModel>> UpdateAsync(Guid id, 
            UpdatePaymentModel update)
        {
            var updateModel = await _databaseContext.Payments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdatePaymentResponseModel>.Failure(
                    new List<string> { "Payment not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.Payments.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdatePaymentResponseModel>.Success(new UpdatePaymentResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
