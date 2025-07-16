using AutoMapper;
using EduPress.Application.Models.CourseSectionModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class CourseSectionMapping : Profile
    {
        public CourseSectionMapping()
        {
            CreateMap<CreateCourseSectionModel, CourseSection>();

            CreateMap<UpdateCourseSectionModel, CourseSection>().ReverseMap();

            CreateMap<CourseSection, CourseSectionResponseModel>();
        }
    }
}
