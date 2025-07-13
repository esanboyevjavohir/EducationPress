namespace EduPress.Application.Models.ContactLocationsModel
{
    public class UpdateLocationsModel
    {
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class UpdateLocationsResponseModel : BaseResponseModel { }
}
