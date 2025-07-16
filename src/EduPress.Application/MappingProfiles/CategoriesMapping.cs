using AutoMapper;
using EduPress.Application.Models.CategoriesModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class CategoriesMapping : Profile
    {
        public CategoriesMapping()
        {
            CreateMap<CreateCategoriesModel, Categories>();

            CreateMap<UpdateCategoriesModel, Categories>().ReverseMap();

            CreateMap<Categories, CategoriesResponseModel>();
        }
    }
}
