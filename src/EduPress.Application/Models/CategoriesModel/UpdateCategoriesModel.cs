namespace EduPress.Application.Models.CategoriesModel
{
    public class UpdateCategoriesModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CourseCount { get; set; }
    }

    public class UpdateCategoriesResponseModel : BaseResponseModel { }
}
