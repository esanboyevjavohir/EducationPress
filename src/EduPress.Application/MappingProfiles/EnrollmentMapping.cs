using AutoMapper;
using EduPress.Application.Models.EnrollmentModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class EnrollmentMapping : Profile
    {
        public EnrollmentMapping()
        {
            CreateMap<CreateEnrollmentModel, Enrollment>();

            CreateMap<UpdateEnrollmentModel, Enrollment>().ReverseMap();

            CreateMap<Enrollment, EnrollmentResponseModel>();
        }
    }
}
