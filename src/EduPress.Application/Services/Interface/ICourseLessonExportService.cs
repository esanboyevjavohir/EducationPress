namespace EduPress.Application.Services.Interface
{
    public interface ICourseLessonExportService
    {
        Task<byte[]> ExportCourseLessonsToExcelAsync();
    }
}
