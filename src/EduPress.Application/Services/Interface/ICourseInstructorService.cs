using EduPress.Application.Models;
using EduPress.Application.Models.CourseInstructorModel;

namespace EduPress.Application.Services.Interface
{
    public interface ICourseInstructorService
    {
        Task<ApiResult<CourseInstructorResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<CourseInstructorResponseModel>>> GetAllAsync();
        Task<ApiResult<CreateCourseInstructorResponseModel>> CreateAsync(CreateCourseInstructorModel create);
        Task<ApiResult<UpdateCourseInstructorResponseModel>> UpdateAsync(Guid id,
            UpdateCourseInstructorModel update);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
    }
}
