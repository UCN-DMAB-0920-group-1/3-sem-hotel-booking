using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.Model.DTO
{
    public static class DTOExtentions
    {
        public static HotelDTO Map(this Hotel hotel)
        {
            if (hotel == null) return null;
            return new HotelDTO()
            {
                Href = hotel.ExtractHref(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationHref = @$"api/Location/{hotel.LocationId}" // TODO should this be changed?
            };
        }

        public static Hotel Map(this HotelDTO hotel)
        {
            if (hotel == null) return null;
            return new Hotel()
            {
                Id = hotel.ExtractId(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationId = GetIdFromHref(hotel.LocationHref) ?? 0
            };
        }

        public static RoomDto Map(this HotelDTO hotel)
        {
            if (hotel == null) return null;
            return new Hotel()
            {
                Id = hotel.ExtractId(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationId = GetIdFromHref(hotel.LocationHref) ?? 0
            };
        }

        public static Hotel Map(this HotelDTO hotel)
        {
            if (hotel == null) return null;
            return new Hotel()
            {
                Id = hotel.ExtractId(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationId = GetIdFromHref(hotel.LocationHref) ?? 0
            };
        }

        public static int? ExtractId(this BaseModelDTO dto)
        {
            return GetIdFromHref(dto.Href);
        }

        public static string ExtractHref(this BaseModel model)
        {
            return $@"/api/{model.GetType().Name.ToLower()}/{model.Id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;

            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
    }
}
