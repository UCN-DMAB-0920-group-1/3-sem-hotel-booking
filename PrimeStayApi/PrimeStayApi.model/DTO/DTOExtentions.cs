using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model.DTO
{
    public static class DTOExtentions
    {
        public static HotelDTO MapToDTO(this Hotel hotel)
        {
            return new HotelDTO()
            {
                Href = @$"api/Hotel/{hotel.Id}",
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationHref = @$"api/Location/{hotel.LocationId}"
            };
        }
    }
}
