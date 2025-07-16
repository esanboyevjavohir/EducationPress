using AutoMapper;
using EduPress.Application.Models.ContactLocationsModel;
using EduPress.Core.Entities;

namespace EduPress.Application.MappingProfiles
{
    public class ContactLocationsMapping : Profile
    {
        public ContactLocationsMapping()
        {
            CreateMap<CreateLocationsModel, ContactLocations>();

            CreateMap<UpdateLocationsModel, ContactLocations>().ReverseMap();

            CreateMap<ContactLocations, LocationsResponseModel>();
        }
    }
}
