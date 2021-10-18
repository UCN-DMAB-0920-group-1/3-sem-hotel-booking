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
                LocationHref = @$"api/Location/{hotel.LocationId}" // TODO use helper method GetHrefFromId()
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

        public static RoomDto Map(this Room room)
        {
            if (room == null) return null;
            return new RoomDto()
            {
                Href = room.ExtractHref(),
                Type = room.Type,
                Num_of_avaliable = room.Num_of_avaliable,
                Num_of_beds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                hotelHref = GetHrefFromId(typeof(Hotel), room.Hotel_Id)
            };
        }

        public static Room Map(this RoomDto room)
        {
            if (room == null) return null;
            return new Room()
            {
                Id = room.ExtractId(),
                Type = room.Type,
                Num_of_avaliable = room.Num_of_avaliable,
                Num_of_beds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = GetIdFromHref(room.hotelHref) ?? 0
            };
        }

        public static int? ExtractId(this BaseModelDTO dto)
        {
            return GetIdFromHref(dto.Href);
        }

        public static string ExtractHref(this BaseModel model)
        {
            return GetHrefFromId(model.GetType(), model.Id);
        }

        public static string GetHrefFromId(Type type, int? id)
        {
            if (id == null) return null;

            return $@"api/{type.Name.ToLower()}/{id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;

            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
    }
}
