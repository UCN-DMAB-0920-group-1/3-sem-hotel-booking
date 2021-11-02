namespace PrimeStay.MVC.DataAccessLayer.DTO
{
    public class LocationDto : BaseDto
    {
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
