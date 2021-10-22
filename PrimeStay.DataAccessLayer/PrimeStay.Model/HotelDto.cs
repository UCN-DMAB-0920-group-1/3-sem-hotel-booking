using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStay.Model
{
    public class HotelDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StaffedHours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }
    }
}
