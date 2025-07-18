using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CourseLessonsModel;
using EduPress.Application.Models.LessonProgressModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class LessonProgressService : ILessonProgressService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public LessonProgressService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateLessonProgressResponseModel>> CreateAsync(
            CreateLessonProgressModel create)
        {
            var createModel = _mapper.Map<LessonProgress>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.LessonProgresses.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateLessonProgressResponseModel>.Success(
                new CreateLessonProgressResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.LessonProgresses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "LessonProgresses not found" });
            }

            _databaseContext.LessonProgresses.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<LessonProgressResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.LessonProgresses
                .AsNoTracking()
                .ProjectTo<LessonProgressResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<LessonProgressResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<LessonProgressResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.LessonProgresses
                .AsNoTracking()
                .ProjectTo<LessonProgressResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<LessonProgressResponseModel>.Failure(
                                new List<string> { "LessonProgresses not found" });
            }

            return ApiResult<LessonProgressResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateLessonProgressResponseModel>> UpdateAsync(Guid id, 
            UpdateLessonProgressModel update)
        {
            var updateModel = await _databaseContext.LessonProgresses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateLessonProgressResponseModel>.Failure(
                    new List<string> { "Categories not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.LessonProgresses.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateLessonProgressResponseModel>.Success(
                new UpdateLessonProgressResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
