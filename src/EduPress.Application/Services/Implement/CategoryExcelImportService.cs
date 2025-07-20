using EduPress.Application.Services.Interface;
using EduPress.Core.Entities;
using EduPress.DataAccess.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace EduPress.Application.Services.Implement
{
    public class CategoryExcelImportService : ICategoryExcelImportService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoryExcelImportService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task ImportFromExcelAsync(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                var name = worksheet.Cells[row, 1].Text;
                var courseCountText = worksheet.Cells[row, 2].Text;

                if (string.IsNullOrWhiteSpace(name) || !int.TryParse(courseCountText, out int courseCount))
                    continue;

                var category = new Categories
                {
                    Name = name,
                    CourseCount = courseCount,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = null
                };

                _databaseContext.Categories.Add(category);
            }

            await _databaseContext.SaveChangesAsync();
        }
    }
}
