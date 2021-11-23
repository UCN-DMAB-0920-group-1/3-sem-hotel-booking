using System;

namespace Models
{
    public class BookingEntity : BaseEntity
    {


        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
        public int? Guests { get; set; }
        public int? Room_id { get; set; }
        public int? Room_type_id { get; set; }
        public int? Customer_id { get; set; }

    }
}
