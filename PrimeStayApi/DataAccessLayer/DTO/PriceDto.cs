using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class PriceDto : BaseModelDto
    {
        public DateTime? startDate { get; set; }
        public int? price { get; set; }
        public int? roomTypeId { get; set; }
    }
}
