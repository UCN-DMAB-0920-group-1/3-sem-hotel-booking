using System;


namespace DataAccessLayer.DTO
{
    public class BookingDto : BaseDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Guests { get; set; }
        public string RoomTypeHref { get; set; }
        public string RoomHref { get; set; }
        public string CustomerHref { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
