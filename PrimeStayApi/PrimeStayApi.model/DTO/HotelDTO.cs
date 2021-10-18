using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model.DTO
{
    public class HotelDTO : BaseModelDTO
    {
        public HotelDTO(int id, string name, string description, string staffed_hours, int? stars, string locationHref) : base(id)
        {
            Name = name;
            Description = description;
            Staffed_hours = staffed_hours;
            Stars = stars;
            LocationHref = $"/api/Location?id={locationHref}";
        }
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
