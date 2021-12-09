using Model;
using System;

namespace DataAccessLayer.DTO
{
    public static class MapperExtension
    {
        public static Hotel Map(this HotelDto hotel)
        {
            return new Hotel()
            {
                Id = hotel.ExtractId(),
                Name = hotel.Name,
                Description = hotel.Description,
                StaffedHours = hotel.StaffedHours,
                LocationId = GetIdFromHref(hotel.LocationHref),
                Stars = hotel.Stars,
                Active = hotel.Active,

            };
        }
        public static HotelDto Map(this Hotel hotel)
        {
            return new HotelDto()
            {
                Href = hotel.ExtractHref(),
                Name = hotel.Name,
                Description = hotel.Description,
                StaffedHours = hotel.StaffedHours,
                LocationHref = GetHrefFromId(typeof(Location), hotel.LocationId),
                Stars = hotel.Stars,
                Active = hotel.Active,

            };
        }
        public static RoomType Map(this RoomTypeDto roomType)
        {
            return new RoomType()
            {
                Id = roomType.ExtractId(),
                Type = roomType.Type,
                Description = roomType.Description,
                Beds = roomType.Beds,
                Rating = roomType.Rating,
                HotelId = GetIdFromHref(roomType.HotelHref),
                Active = roomType.Active,
            };
        }
        public static RoomTypeDto Map(this RoomType roomType)
        {
            return new RoomTypeDto()
            {
                Href = roomType.ExtractHref(),
                Type = roomType.Type,
                Description = roomType.Description,
                Beds = roomType.Beds,
                Rating = roomType.Rating,
                HotelHref = GetHrefFromId(typeof(RoomType), roomType.HotelId),
                Active = roomType.Active,
            };
        }
        public static Customer Map(this CustomerDto customer)
        {
            if (customer is null) return null;
            return new Customer()
            {
                Id = customer.ExtractId(),
                BirthDay = customer.BirthDay,
                Email = customer.Email,
                Name = customer.Name,
                Phone = customer.Phone,
                UserId = customer.UserId,
            };
        }
        public static CustomerDto Map(this Customer customer)
        {
            if (customer is null) return null;
            return new CustomerDto()
            {
                Href = customer.ExtractHref(),
                Email = customer.Email,
                BirthDay = customer.BirthDay,
                Name = customer.Name,
                Phone = customer.Phone,
                UserId = customer.UserId,
            };

        }
        public static RoomDto Map(this Room room)
        {
            if (room is null) return null;
            return new RoomDto()
            {
                Href = room.ExtractHref(),
                Notes = room.Notes,
                Active = room.Active,
                Room_number = room.RoomNumber,
                RoomTypeHref = GetHrefFromId(typeof(RoomType), room.RoomTypeId),
            };
        }
        public static Room Map(this RoomDto room)
        {
            if (room is null) return null;
            return new Room()
            {
                Id = room.ExtractId(),
                Active = room.Active,
                Notes = room.Notes,
                RoomNumber = room.Room_number,
                RoomTypeId = GetIdFromHref(room.RoomTypeHref),
            };
        }
        public static Booking Map(this BookingDto booking)
        {
            if (booking is null) return null;
            return new Booking()
            {
                Id = booking.ExtractId(),
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Guests = booking.Guests,
                CustomerId = GetIdFromHref(booking.CustomerHref),
                RoomTypeId = GetIdFromHref(booking.RoomTypeHref),
                RoomId = GetIdFromHref(booking.RoomHref),
            };
        }
        public static BookingDto Map(this Booking booking)
        {
            if (booking is null) return null;
            return new BookingDto()
            {
                Href = booking.ExtractHref(),
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Guests = booking.Guests,
                CustomerHref = GetHrefFromId(typeof(Customer), booking.CustomerId),
                RoomTypeHref = GetHrefFromId(typeof(RoomType), booking.RoomTypeId),
                RoomHref = GetHrefFromId(typeof(Room), booking.RoomId),

            };
        }

        #region helperMethods
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
            _ = int.TryParse(href[(href.LastIndexOf("/") + 1)..], out int result);
            return result;
        }
        #endregion
    }
}
