using System;


namespace Models.DTO
{
    public class BookingDto : BaseModelDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Guests { get; set; }
        public string RoomHref { get; set; }
        public string RoomTypeHref { get; set; }
        public string CustomerHref { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
