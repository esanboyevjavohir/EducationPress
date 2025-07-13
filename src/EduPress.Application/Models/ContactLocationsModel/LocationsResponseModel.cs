namespace EduPress.Application.Models.ContactLocationsModel
{
    public class LocationsResponseModel : BaseResponseModel
    {
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
