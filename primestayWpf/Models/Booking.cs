using System;

namespace Model
{
    public class Booking : BaseModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Guests { get; set; }
        public int? RoomTypeId { get; set; }
        public int? RoomId { get; set; }
        public int? CustomerId { get; set; }
    }
}
