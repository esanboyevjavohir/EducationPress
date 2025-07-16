using AutoMapper;
using EduPress.Application.Models.ReviewModel;
using EduPress.Application.Models.ReviewRepliesModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class ReviewRepliesMapping : Profile
    {
        public ReviewRepliesMapping()
        {
            CreateMap<CreateReviewRepliesModel, ReviewReplies>();

            CreateMap<UpdateReviewRepliesModel, ReviewReplies>().ReverseMap();

            CreateMap<ReviewReplies, ReviewRepliesResponseModel>();
        }
    }
}
