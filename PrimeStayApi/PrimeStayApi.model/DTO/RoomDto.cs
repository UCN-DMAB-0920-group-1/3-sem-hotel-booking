using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model.DTO
{
    public class RoomDto : BaseModelDto
    {
        public string Type { get; set; }
        public int? NumOfAvaliable { get; set; }
        public int? NumOfBeds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public string hotelHref { get; set; }
    }
}
