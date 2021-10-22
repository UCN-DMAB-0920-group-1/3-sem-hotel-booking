using PrimeStay.Model;
using System;

namespace primestayMVC.Model
{
    public static class ModelMapper
    {
        public static Hotel Map(this HotelDal hotel)
        {
            if (hotel == null) return null;
            return new Hotel()
            {
                Href = hotel.ExtractHref(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationHref = GetHrefFromId(typeof(Hotel), hotel.Location_Id),
            };
        }

        public static HotelDal Map(this Hotel hotel)
        {
            if (hotel == null) return null;
            return new HotelDal()
            {
                Id = hotel.ExtractId(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                Location_Id = GetIdFromHref(hotel.LocationHref) ?? 0
            };
        }

        public static Room Map(this RoomDal room)
        {
            if (room == null) return null;
            return new Room()
            {
                Id = room.Id,
                Href = GetHrefFromId(typeof(Room), room.Id),
                Type = room.Type,
                Num_of_avaliable = room.Num_of_avaliable,
                Num_of_beds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = room.Hotel_Id,
            };
        }

        public static int? ExtractId(this BaseModel dto)
        {
            return GetIdFromHref(dto.Href);
        }

        public static string ExtractHref(this BaseModelDal model)
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
