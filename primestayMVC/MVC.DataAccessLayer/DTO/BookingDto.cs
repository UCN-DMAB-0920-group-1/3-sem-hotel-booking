using System;


namespace PrimeStay.MVC.DataAccessLayer.DTO
{
    public class BookingDto : BaseDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Guests { get; set; }
        public string RoomHref { get; set; }
        public string CustomerHref { get; set; }
    }
}
