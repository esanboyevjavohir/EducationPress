using AutoMapper;
using EduPress.Application.Models.ReviewModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class ReviewMapping : Profile
    {
        public ReviewMapping()
        {
            CreateMap<CreateReviewModel, Review>();

            CreateMap<UpdateReviewModel, Review>().ReverseMap();

            CreateMap<Review, ReviewResponseModel>();
        }
    }
}
