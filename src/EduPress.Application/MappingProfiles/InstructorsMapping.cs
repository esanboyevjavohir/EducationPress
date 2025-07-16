using AutoMapper;
using EduPress.Application.Models.InstructorsModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class InstructorsMapping : Profile
    {
        public InstructorsMapping()
        {
            CreateMap<CreateInstructorsModel, Instructors>();

            CreateMap<UpdateInstructorsModel, Instructors>().ReverseMap();

            CreateMap<Instructors, InstructorsResponseModel>();
        }
    }
}
