using AutoMapper;
using EduPress.Application.Models.LessonProgressModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class LessonProgressMapping : Profile
    {
        public LessonProgressMapping()
        {
            CreateMap<CreateLessonProgressModel, LessonProgress>();

            CreateMap<UpdateLessonProgressModel, LessonProgress>().ReverseMap();

            CreateMap<LessonProgress, LessonProgressResponseModel>();
        }
    }
}
