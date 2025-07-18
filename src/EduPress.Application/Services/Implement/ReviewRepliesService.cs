using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.ReviewModel;
using EduPress.Application.Models.ReviewRepliesModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class ReviewRepliesService : IReviewRepliesService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public ReviewRepliesService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateReviewRepliesResponseModel>> CreateAsync(
            CreateReviewRepliesModel create)
        {
            var createModel = _mapper.Map<ReviewReplies>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.ReviewReplies.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateReviewRepliesResponseModel>.Success(new CreateReviewRepliesResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.ReviewReplies
                .FirstOrDefaultAsync(x => x.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "ReviewReplies not found" });
            }

            _databaseContext.ReviewReplies.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<ReviewRepliesResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.ReviewReplies
                .AsNoTracking()
                .ProjectTo<ReviewRepliesResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<ReviewRepliesResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<ReviewRepliesResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.ReviewReplies
                .AsNoTracking()
                .ProjectTo<ReviewRepliesResponseModel>( _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (getById == null)
            {
                return ApiResult<ReviewRepliesResponseModel>.Failure(
                                new List<string> { "ReviewReplies not found" });
            }

            return ApiResult<ReviewRepliesResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateReviewRepliesResponseModel>> UpdateAsync(Guid id, 
            UpdateReviewRepliesModel update)
        {
            var updateModel = await _databaseContext.ReviewReplies
                .FirstOrDefaultAsync(r => r.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateReviewRepliesResponseModel>.Failure(
                    new List<string> { "ReviewReplies not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.ReviewReplies.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateReviewRepliesResponseModel>.Success(
                new UpdateReviewRepliesResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
