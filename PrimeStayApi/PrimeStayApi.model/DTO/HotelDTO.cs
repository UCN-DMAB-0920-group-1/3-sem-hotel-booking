using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model.DTO
{
    internal class HotelDTO : BaseModelDTO
    {
        public HotelDTO(int? id, string name, string description, string staffed_hours, int? stars, string locationHref) : base($"/api/hotel?id={id}")
        {
            Name = name;
            Description = description;
            Staffed_hours = staffed_hours;
            Stars = stars;
            LocationHref = $"/api/Location?id={locationHref}";
        }

        public string Name  { get; set; }
        public string Description { get; set; }
        public string Staffed_hours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }
       
    }
}
