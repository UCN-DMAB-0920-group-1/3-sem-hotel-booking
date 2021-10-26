using PrimeStay.MVC.DataAccessLayer.DTO;
using System;

namespace PrimeStay.MVC.Model
{
    public static class ModelMapper
    {
        public static Hotel Map(this HotelDto hotel)
        {
            if (hotel == null) return null;
            return new Hotel()
            {
                Id = GetIdFromHref(hotel.Href),
                Href = hotel.Href,
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.Staffed_Hours,
                Stars = hotel.Stars,
                Location_Id = GetIdFromHref(hotel.LocationHref)
            };
        }

        public static HotelDto Map(this Hotel hotel)
        {
            if (hotel == null) return null;
            return new HotelDto()
            {
                Href = GetHrefFromId(typeof(Hotel), hotel.Id),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_Hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationHref = GetHrefFromId(typeof(Hotel), hotel.Id),
            };
        }

        public static Room Map(this RoomDto room)
        {
            if (room == null) return null;
            return new Room()
            {
                Id = GetIdFromHref(room.Href),
                Href = room.Href,
                Type = room.Type,
                Num_of_avaliable = room.Num_of_avaliable,
                Num_of_beds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = room.Hotel_Id,
            };
        }
        public static RoomDto Map(this Room room)
        {
            if (room == null) return null;
            return new RoomDto()
            {
                Href = GetHrefFromId(typeof(Room), room.Id),
                Type = room.Type,
                Num_of_avaliable = room.Num_of_avaliable,
                Num_of_beds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = room.Hotel_Id,
            };
        }
        public static Location Map(this LocationDto location)
        {
            if (location == null) return null;
            return new Location()
            {
                Id = GetIdFromHref(location.Href),
                Street_Address = location.Street_Address,
                City = location.City,
                Country = location.Country,
                Zip_code = location.Zip_code,
            };
        }

        public static LocationDto Map(this Location location)
        {
            if (location == null) return null;
            return new LocationDto()
            {
                Href = GetHrefFromId(typeof(Location), location.Id),
                Street_Address = location.Street_Address,
                City = location.City,
                Country = location.Country,
                Zip_code = location.Zip_code,
            };
        }

        public static BookingDto Map(this Booking booking)
        {
            if (booking == null) return null;
            return new BookingDto()
            {
                Href = booking.Href,
                End_date = booking.End_date,
                Start_date = booking.Start_date,
                Customer_href = booking.Customer_href,
                Num_of_guests = booking.Num_of_guests,
                Room_href = booking.Room_href,
            };
        }

        public static Booking Map(this BookingDto booking)
        {
            if (booking == null) return null;
            return new Booking()
            {
                Id = booking.ExtractId(),
                Href = booking.Href,
                Customer_href = booking.Customer_href,
                End_date = booking.End_date,
                Start_date = booking.Start_date,
                Num_of_guests = booking.Num_of_guests,
                Room_href = booking.Room_href,

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
            string typeName = type.Name;

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
