namespace EduPress.Application.Models.ContactLocationsModel
{
    public class CreateLocationsModel
    {
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class CreateLocationsResponseModel : BaseResponseModel { }
}
