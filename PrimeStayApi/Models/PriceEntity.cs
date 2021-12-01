using System;


namespace Models
{
    public class PriceEntity : BaseEntity
    {
        public DateTime? start_date { get; set; }
        public int? price { get; set; }
        public int? room_type_id { get; set; }
    }
}
