using AutoMapper;
using EduPress.Application.Models.Payment;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class PaymentMapping : Profile
    {
        public PaymentMapping()
        {
            CreateMap<CreatePaymentModel, Payment>();

            CreateMap<UpdatePaymentModel, Payment>().ReverseMap();

            CreateMap<Payment, PaymentResponseModel>();
        }
    }
}
