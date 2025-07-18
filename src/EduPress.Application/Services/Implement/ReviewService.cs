using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Models.ReviewModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public ReviewService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateReviewResponseModel>> CreateAsync(CreateReviewModel create)
        {
            var createModel = _mapper.Map<Review>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.Reviews.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateReviewResponseModel>.Success(new CreateReviewResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.Reviews
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "Review not found" });
            }

            _databaseContext.Reviews.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<ReviewResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.Reviews
                .AsNoTracking()
                .ProjectTo<ReviewResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<ReviewResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<ReviewResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.Reviews
                .AsNoTracking()
                .ProjectTo<ReviewResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<ReviewResponseModel>.Failure(
                                new List<string> { "Review not found" });
            }

            return ApiResult<ReviewResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateReviewResponseModel>> UpdateAsync(Guid id, 
            UpdateReviewModel update)
        {
            var updateModel = await _databaseContext.Reviews
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateReviewResponseModel>.Failure(
                    new List<string> { "Review not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.Reviews.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateReviewResponseModel>.Success(new UpdateReviewResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
