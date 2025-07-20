using Microsoft.AspNetCore.Http;

namespace EduPress.Application.Services.Interface
{
    public interface ICategoryExcelImportService
    {
        Task ImportFromExcelAsync(IFormFile file);
    }
}
