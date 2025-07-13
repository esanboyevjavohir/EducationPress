namespace EduPress.Application.Models.CategoriesModel
{
    public class CreateCategoriesModel
    {
        public string Name { get; set; }
        public int CourseCount { get; set; }
    }

    public class CreateCategoriesResponseModel : BaseResponseModel { }
}
