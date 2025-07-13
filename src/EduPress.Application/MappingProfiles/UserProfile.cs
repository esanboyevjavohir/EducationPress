using AutoMapper;
using EduPress.Application.Models.User;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserModel, User>().ReverseMap();
            CreateMap<ForgotPasswordModel, User>().
                ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<ResetPasswordModel, User>()
             .ForMember(dest => dest.ResetPasswordToken, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserResponseModel>();
        }
    }
}
