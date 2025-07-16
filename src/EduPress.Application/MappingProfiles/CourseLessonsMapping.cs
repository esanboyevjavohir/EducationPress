using AutoMapper;
using EduPress.Application.Models.CourseLessonsModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class CourseLessonsMapping : Profile
    {
        public CourseLessonsMapping()
        {
            CreateMap<CreateCourseLessonsModel, CourseLessons>();

            CreateMap<UpdateCourseLessonsModel, CourseLessons>().ReverseMap();

            CreateMap<CourseLessons, CourseLessonsResponseModel>();
        }
    }
}
