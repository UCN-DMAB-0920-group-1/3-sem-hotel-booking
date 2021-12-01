using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Price :BaseModel
    {
        public int price { get; set; }
        public DateTime start_date { get; set; }
        public int roomTypeId { get; set; }

    }
}
