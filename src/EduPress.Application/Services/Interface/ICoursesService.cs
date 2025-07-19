using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models;
using EduPress.Application.Models.CoursesModel;
using StatusGeneric;

namespace EduPress.Application.Services.Interface
{
    public interface ICoursesService
    {
        Task<IStatusGeneric<CoursesResponseModel>> GetByIdAsync(Guid id);
        Task<IStatusGeneric<List<CoursesResponseModel>>> GetAllAsync();
        Task<IStatusGeneric<CreateCoursesResponseModel>> CreateAsync(CreateCoursesModel create);
        Task<IStatusGeneric<UpdateCoursesResponseModel>> UpdateAsync(Guid id, UpdateCoursesModel update);
        Task<IStatusGeneric<bool>> DeleteAsync(Guid id);
    }
}



