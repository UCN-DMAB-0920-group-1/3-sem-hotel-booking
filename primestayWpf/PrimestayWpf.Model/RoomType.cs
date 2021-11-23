using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RoomType : BaseModel
    {
        public string Type { get; set; }
        public int? Num_of_avaliable { get; set; }
        public int? Beds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public string HotelHref { get; set; }
        public bool? Active { get; set; }
    }
}
