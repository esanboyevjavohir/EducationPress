using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CourseLessonsModel;
using EduPress.Application.Models.CourseSectionModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class CourseSectionService : ICourseSectionService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public CourseSectionService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateCourseSectionResponseModel>> CreateAsync(CreateCourseSectionModel create)
        {
            var createModel = _mapper.Map<CourseSection>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.CourseSections.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateCourseSectionResponseModel>.Success(new CreateCourseSectionResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.CourseSections
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "CourseSections not found" });
            }

            _databaseContext.CourseSections.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<CourseSectionResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.CourseSections
                .AsNoTracking()
                .ProjectTo<CourseSectionResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<CourseSectionResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<CourseSectionResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.CourseSections
                .AsNoTracking()
                .ProjectTo<CourseSectionResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<CourseSectionResponseModel>.Failure(
                                new List<string> { "CourseSections not found" });
            }

            return ApiResult<CourseSectionResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateCourseSectionResponseModel>> UpdateAsync(Guid id, 
            UpdateCourseSectionModel update)
        {
            var updateModel = await _databaseContext.CourseSections
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateCourseSectionResponseModel>.Failure(
                    new List<string> { "CourseSections not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.CourseSections.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateCourseSectionResponseModel>.Success(new UpdateCourseSectionResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
