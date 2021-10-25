using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model
{
    public class BookingEntity:BaseEntity
    {
        public DateTime? Start_date { get; set; }
        public DateTime? End_date { get; set; }
        public int? Num_of_guests { get; set; }
        public int? Room_id { get; set; }
        public int? Customer_id { get; set; }

    }
}
