using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CourseLessonsModel;
using EduPress.Application.Models.EnrollmentModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public EnrollmentService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateEnrollmentResponseModel>> CreateAsync(CreateEnrollmentModel create)
        {
            var createModel = _mapper.Map<Enrollment>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.Enrollments.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateEnrollmentResponseModel>.Success(new CreateEnrollmentResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.Enrollments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "Enrollment not found" });
            }

            _databaseContext.Enrollments.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<EnrollmentResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.Enrollments
                .AsNoTracking()
                .ProjectTo<EnrollmentResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<EnrollmentResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<EnrollmentResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.Enrollments
                .AsNoTracking()
                .ProjectTo<EnrollmentResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<EnrollmentResponseModel>.Failure(
                                new List<string> { "Enrollment not found" });
            }

            return ApiResult<EnrollmentResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateEnrollmentResponseModel>> UpdateAsync(Guid id, UpdateEnrollmentModel update)
        {
            var updateModel = await _databaseContext.Enrollments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateEnrollmentResponseModel>.Failure(
                    new List<string> { "Categories not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.Enrollments.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateEnrollmentResponseModel>.Success(new UpdateEnrollmentResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
