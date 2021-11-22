using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimestayWpf.Model
{
    public class RoomType : BaseModel
    {
        public string Type { get; set; }
        public int? Beds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public string? HotelHref { get; set; }
    }
}
