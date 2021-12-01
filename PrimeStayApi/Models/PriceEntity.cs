using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PriceEntity : BaseEntity
    {
        public DateTime? start_date { get; set; }
        public int? price { get; set; }
        public int? room_type_id { get; set; }
    }
}
