using Models;
using System;

namespace DataAccessLayer.DTO
{
    public static class MapperExtension
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
                StaffedHours = hotel.StaffedHours,
                Stars = hotel.Stars,
                LocationId = GetIdFromHref(hotel.LocationHref)
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
                StaffedHours = hotel.StaffedHours,
                Stars = hotel.Stars,
                LocationHref = GetHrefFromId(typeof(Hotel), hotel.Id),
            };
        }

        public static Room Map(this RoomTypeDto room)
        {
            if (room == null) return null;
            return new Room()
            {
                Id = GetIdFromHref(room.Href),
                Href = room.Href,
                Type = room.Type,
                Avaliable = room.Avaliable,
                Beds = room.Beds,
                Description = room.Description,
                Rating = room.Rating,
                HotelId = room.HotelId,
            };
        }
        public static RoomTypeDto Map(this Room room)
        {
            if (room == null) return null;
            return new RoomTypeDto()
            {
                Href = GetHrefFromId(typeof(Room), room.Id),
                Type = room.Type,
                Avaliable = room.Avaliable,
                Beds = room.Beds,
                Description = room.Description,
                Rating = room.Rating,
                HotelId = room.HotelId,
            };
        }

        /*
         * 
         * */

        public static Price Map(this PriceDto price)
        {
            if (price == null) return null;
            return new Price()
            {
                Id = GetIdFromHref(price.Href),
                Value = price.Value,
                Room_Type_Id = price.RoomTypeId,
                Start_Date = price.StartDate,
            };
        }
        public static PriceDto Map(this Price price)
        {
            if (price == null) return null;
            return new PriceDto()
            {
                Href = GetHrefFromId(typeof(Price), price.Id),
                Value = price.Value,
                RoomTypeId = price.Room_Type_Id,
                StartDate = price.Start_Date,
            };
        }
        /**
         * 
         * */
        public static Location Map(this LocationDto location)
        {
            if (location == null) return null;
            return new Location()
            {
                Id = GetIdFromHref(location.Href),
                StreetAddress = location.StreetAddress,
                City = location.City,
                Country = location.Country,
                ZipCode = location.ZipCode,
                Lat = location.Lat,
                Lng = location.Lng,
            };
        }

        public static LocationDto Map(this Location location)
        {
            if (location == null) return null;
            return new LocationDto()
            {
                Href = GetHrefFromId(typeof(Location), location.Id),
                StreetAddress = location.StreetAddress,
                City = location.City,
                Country = location.Country,
                ZipCode = location.ZipCode,
                Lat = location.Lat,
                Lng = location.Lng,
            };
        }

        public static BookingDto Map(this Booking booking)
        {
            if (booking == null) return null;
            return new BookingDto()
            {
                Href = booking.Href,
                EndDate = booking.EndDate,
                StartDate = booking.StartDate,
                CustomerHref = GetHrefFromId(typeof(Customer), booking.CustomerId),
                Guests = booking.Guests,
                RoomTypeHref = booking.RoomTypeHref,
            };
        }

        public static Booking Map(this BookingDto booking)
        {
            if (booking == null) return null;
            return new Booking()
            {
                Id = booking.ExtractId(),
                Href = booking.Href,
                CustomerId = GetIdFromHref(booking.CustomerHref),
                EndDate = booking.EndDate,
                StartDate = booking.StartDate,
                Guests = booking.Guests,
                RoomTypeHref = booking.RoomTypeHref,


            };
        }
        public static Customer Map(this CustomerDto customer)
        {
            if (customer == null) return null;
            return new Customer()
            {
                Id = customer.ExtractId(),
                Email = customer.Email,
                Birthday = customer.Birthday,
                Name = customer.Name,
                Phone = customer.Phone,


            };
        }
        public static CustomerDto Map(this Customer customer)
        {
            if (customer == null) return null;
            return new CustomerDto()
            {
                Href = customer.Href,
                Email = customer.Email,
                Birthday = customer.Birthday,
                Name = customer.Name,
                Phone = customer.Phone,
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
