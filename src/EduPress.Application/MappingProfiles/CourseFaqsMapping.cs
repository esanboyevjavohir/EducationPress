using AutoMapper;
using EduPress.Application.Models.CourseFaqsModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class CourseFaqsMapping : Profile
    {
        public CourseFaqsMapping()
        {
            CreateMap<CreateCourseFaqsModel, CourseFaqs>();

            CreateMap<UpdateCourseFaqsModel, CourseFaqs>().ReverseMap();

            CreateMap<CourseFaqs, CourseFaqsResponseModel>();
        }
    }
}
