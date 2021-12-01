using System;

namespace DataAccessLayer.DTO
{
    public class PriceDto : BaseDto
    {
        public int price { get; set; }
        public DateTime startDate { get; set; }
        public int roomTypeId { get; set; }
    }
}
