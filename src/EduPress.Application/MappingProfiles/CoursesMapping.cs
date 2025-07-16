using AutoMapper;
using EduPress.Application.Models.CoursesModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class CoursesMapping : Profile
    {
        public CoursesMapping()
        {
            CreateMap<CreateCoursesModel, Courses>();

            CreateMap<UpdateCoursesModel, Courses>().ReverseMap();

            CreateMap<Courses, CoursesResponseModel>();
        }
    }
}
