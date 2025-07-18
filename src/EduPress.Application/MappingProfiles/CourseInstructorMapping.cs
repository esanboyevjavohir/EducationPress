using AutoMapper;
using EduPress.Application.Models.CourseInstructorModel;
using EduPress.Application.Models.InstructorsModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class CourseInstructorMapping : Profile
    {
        public CourseInstructorMapping()
        {
            CreateMap<CreateCourseInstructorModel, CourseInstructor>();

            CreateMap<UpdateCourseInstructorModel, CourseInstructor>().ReverseMap();

            CreateMap<CourseInstructor, CourseInstructorResponseModel>();
        }
    }
}
