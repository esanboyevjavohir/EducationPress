using AutoMapper;
using AutoMapper.QueryableExtensions;
using EduPress.Application.Models;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CourseInstructorModel;
using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduPress.Application.Services.Implement
{
    public class CourseInstructorService : ICourseInstructorService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public CourseInstructorService(IMapper mapper, DatabaseContext dbContext)
        {
            _mapper = mapper;
            _databaseContext = dbContext;
        }

        public async Task<ApiResult<CreateCourseInstructorResponseModel>> CreateAsync(CreateCourseInstructorModel create)
        {
            var createModel = _mapper.Map<CourseInstructor>(create);
            createModel.CreatedOn = DateTime.Now;

            _databaseContext.CourseInstructors.Add(createModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<CreateCourseInstructorResponseModel>.Success(new CreateCourseInstructorResponseModel
            {
                Id = createModel.Id
            });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var delete = await _databaseContext.CourseInstructors
                .FirstOrDefaultAsync(c => c.Id == id);

            if (delete == null)
            {
                return ApiResult<bool>.Failure(new List<string> { "CourseInstructors not found" });
            }

            _databaseContext.CourseInstructors.Remove(delete);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<List<CourseInstructorResponseModel>>> GetAllAsync()
        {
            var courseInstructor = await _databaseContext.CourseInstructors
                .AsNoTracking()
                .ProjectTo<CourseInstructorResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return ApiResult<List<CourseInstructorResponseModel>>.Success(courseInstructor);
        }

        public async Task<ApiResult<CourseInstructorResponseModel>> GetByIdAsync(Guid id)
        {
            var getById = await _databaseContext.CourseInstructors
                .AsNoTracking()
                .ProjectTo<CourseInstructorResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (getById == null)
            {
                return ApiResult<CourseInstructorResponseModel>.Failure(
                                new List<string> { "CourseInstructor not found" });
            }

            return ApiResult<CourseInstructorResponseModel>.Success(getById);
        }

        public async Task<ApiResult<UpdateCourseInstructorResponseModel>> UpdateAsync(Guid id, 
            UpdateCourseInstructorModel update)
        {
            var updateModel = await _databaseContext.CourseInstructors
                .FirstOrDefaultAsync(c => c.Id == id);

            if (updateModel == null)
            {
                return ApiResult<UpdateCourseInstructorResponseModel>.Failure(
                    new List<string> { "Categories not found" });
            }

            _mapper.Map(update, updateModel);
            updateModel.UpdatedOn = DateTime.Now;
            _databaseContext.CourseInstructors.Update(updateModel);
            await _databaseContext.SaveChangesAsync();

            return ApiResult<UpdateCourseInstructorResponseModel>.Success(new UpdateCourseInstructorResponseModel
            {
                Id = updateModel.Id
            });
        }
    }
}
