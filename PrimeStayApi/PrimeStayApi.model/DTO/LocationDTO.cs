namespace PrimeStayApi.Model.DTO
{
    public class LocationDto : BaseModelDto
    {
        public string Street_Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip_code { get; set; }
    }
}
