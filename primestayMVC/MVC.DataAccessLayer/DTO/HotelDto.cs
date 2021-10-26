namespace PrimeStay.MVC.DataAccessLayer.DTO
{
    public class HotelDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StaffedHours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }
    }
}
