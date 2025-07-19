using EduPress.Application.Services.Interface;
using EduPress.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel;

namespace EduPress.Application.Services.Implement
{
    public class CourseLessonExportService : ICourseLessonExportService
    {
        private readonly DatabaseContext _databaseContext;

        public CourseLessonExportService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<byte[]> ExportCourseLessonsToExcelAsync()
        {
            var lessons = await _databaseContext.CourseLessons
                .AsNoTracking()
                .OrderBy(l => l.CreatedOn)
                .ToListAsync();

            using var package = new ExcelPackage();
            var workSheet = package.Workbook.Worksheets.Add("Course Lessons");

            workSheet.Cells[1, 1].Value = "No";
            workSheet.Cells[1, 2].Value = "Title";
            workSheet.Cells[1, 3].Value = "Duration (minutes)";
            workSheet.Cells[1, 4].Value = "Is Free";
            workSheet.Cells[1, 5].Value = "Course Section ID";
            workSheet.Cells[1, 6].Value = "Created On";
            workSheet.Cells[1, 7].Value = "Updated On";

            for(int i = 0; i < lessons.Count; i++)
            {
                var row = i + 2;
                var lesson = lessons[i];

                workSheet.Cells[row, 1].Value = i + 1;
                workSheet.Cells[row, 2].Value = lesson.Title;
                workSheet.Cells[row, 3].Value = lesson.DurationMinutes.TotalMinutes; // TimeSpan dan double
                workSheet.Cells[row, 4].Value = lesson.IsFree ? "Yes" : "No";
                workSheet.Cells[row, 5].Value = lesson.CourseSectionId.ToString();
                workSheet.Cells[row, 6].Value = lesson.CreatedOn.ToString("yyyy-MM-dd HH:mm");
                workSheet.Cells[row, 7].Value = lesson.UpdatedOn?.ToString("yyyy-MM-dd HH:mm") ?? "-";
            }

            workSheet.Cells.AutoFitColumns();

            return await package.GetAsByteArrayAsync();
        }
    }
}
