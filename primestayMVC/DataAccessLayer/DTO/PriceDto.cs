using System;

namespace DataAccessLayer.DTO
{
    public class PriceDto : BaseDto
    {
        public int Value { get; set; }
        public DateTime StartDate { get; set; }
        public int RoomTypeId { get; set; }
    }
}
