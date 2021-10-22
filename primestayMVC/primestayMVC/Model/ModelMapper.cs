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
                LocationHref = GetHrefFromId(typeof(Location), hotel.Location_Id)
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
        public static RoomDal Map(this Room room)
        {
            if (room == null) return null;
            return new RoomDal()
            {
                Id = room.Id,
                Type = room.Type,
                Num_of_avaliable = room.Num_of_avaliable,
                Num_of_beds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = room.Hotel_Id,
            };
        }
        public static Location Map(this LocationDal location)
        {
            if (location == null) return null;
            return new Location()
            {
                Href = location.ExtractHref(),
                Id = location.Id,
                Street_Address = location.Street_Address,
                City = location.City,
                Country = location.Country,
                Zip_code = location.Zip_code,
            };
        }

        public static LocationDal Map(this Location location)
        {
            if (location == null) return null;
            return new LocationDal()
            {
                Id = location.Id,
                Street_Address = location.Street_Address,
                City = location.City,
                Country = location.Country,
                Zip_code = location.Zip_code,
            };
        }




        #region info_extraction

        public static int? ExtractId(this BaseDto dto)
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
            string typeName = type.Name.Substring(0, (type.Name.Length - 3));

            return $@"api/{typeName}/{id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;

            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
        #endregion
    }
}
