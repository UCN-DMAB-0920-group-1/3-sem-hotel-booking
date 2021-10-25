namespace PrimeStay.MVC.DataAccessLayer.DTO
{
    public class LocationDto : BaseDto
    {
        public string Street_Address { get; set; }
        public string Zip_code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
