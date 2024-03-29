﻿using System;

namespace Models
{
    public class Booking : BaseModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Guests { get; set; }
        public int? RoomTypeId { get; set; }
        public int? CustomerId { get; set; }
    }
}
