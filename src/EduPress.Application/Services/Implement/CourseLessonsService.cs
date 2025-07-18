using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CourseLessonsModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class CourseLessonsService : ICourseLessonsService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public CourseLessonsService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateCourseLessonsResponseModel>> CreateAsync(CreateCourseLessonsModel create)
        {
            var createModel = _mapper.Map<CourseLessons>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.CourseLessons.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateCourseLessonsResponseModel>.Success(new CreateCourseLessonsResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.CourseLessons
                    .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "CourseLessons not found" });
            }

            _databaseContext.CourseLessons.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<CourseLessonsResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.CourseLessons
                .AsNoTracking()
                .ProjectTo<CourseLessonsResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<CourseLessonsResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<CourseLessonsResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.CourseLessons
                .AsNoTracking()
                .ProjectTo<CourseLessonsResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<CourseLessonsResponseModel>.Failure(
                                new List<string> { "CourseLessons not found" });
            }

            return ApiResult<CourseLessonsResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateCourseLessonsResponseModel>> UpdateAsync(Guid id, 
            UpdateCourseLessonsModel update)
        {
            var updateModel = await _databaseContext.CourseLessons
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateCourseLessonsResponseModel>.Failure(
                    new List<string> { "CourseLessons not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.CourseLessons.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateCourseLessonsResponseModel>.Success(new UpdateCourseLessonsResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
