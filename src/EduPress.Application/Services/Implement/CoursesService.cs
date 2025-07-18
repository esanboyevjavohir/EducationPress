using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class CoursesService : ICoursesService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public CoursesService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper;
            _databaseContext = databaseContext;
        }

        public async Task<ApiResult<CreateCoursesResponseModel>> CreateAsync(CreateCoursesModel create)
        {
            var createModel = _mapper.Map<Courses>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.Courses.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateCoursesResponseModel>.Success(new CreateCoursesResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "Courses not found" });
            }

            _databaseContext.Courses.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<CoursesResponseModel>>> GetAllAsync()
        {
            var getAll = await _databaseContext.Courses
                   .AsNoTracking()
                   .ProjectTo<CoursesResponseModel>(_mapper.ConfigurationProvider)
                   .ToListAsync();

            return ApiResult<List<CoursesResponseModel>>.Success(getAll);
        }

        public async Task<ApiResult<CoursesResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.Courses
                .AsNoTracking()
                .ProjectTo<CoursesResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(getById == null)
            {
                return ApiResult<CoursesResponseModel>.Failure(
                                new List<string> { "Courses not found" });
            }

            return ApiResult<CoursesResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateCoursesResponseModel>> UpdateAsync(Guid id, 
            UpdateCoursesModel update)
        {
            var updateModel = await _databaseContext.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateCoursesResponseModel>.Failure(
                    new List<string> { "Courses not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.Courses.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateCoursesResponseModel>.Success(new UpdateCoursesResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
