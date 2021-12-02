using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class PriceDto : BaseModelDto
    {
        public DateTime? StartDate { get; set; }
        public int? Value { get; set; }
        public int? RoomTypeId { get; set; }
    }
}
