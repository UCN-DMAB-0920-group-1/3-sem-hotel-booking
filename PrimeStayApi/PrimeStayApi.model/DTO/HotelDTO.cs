using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model.DTO
{
    public class HotelDTO : BaseModelDTO
    {
      
        public HotelDTO() : base()
        {

        }
        public string Name  { get; set; }
        public string Description { get; set; }
        public string Staffed_hours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }
       
    }
}
