using Models;
using System;

namespace DataAccessLayer.DTO
{
    public static class MapperExtension
    {
        public static HotelDto Map(this HotelEntity hotel)
        {
            if (hotel == null) return null;
            return new HotelDto()
            {
                Href = hotel.ExtractHref(),
                Name = hotel.Name,
                Description = hotel.Description,
                StaffedHours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationHref = GetHrefFromId(typeof(LocationEntity), hotel.Location_Id),
                Active = hotel.Active,
            };
        }

        public static HotelEntity Map(this HotelDto hotel)
        {
            if (hotel == null) return null;
            return new HotelEntity()
            {
                Id = hotel.ExtractId(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.StaffedHours,
                Stars = hotel.Stars,
                Location_Id = GetIdFromHref(hotel.LocationHref),
                Active = hotel.Active,
            };
        }
        public static BookingEntity Map(this BookingDto booking)
        {
            if (booking == null) return null;
            return new BookingEntity()
            {
                Id = booking.ExtractId(),
                Start_date = booking.StartDate,
                End_date = booking.EndDate,
                Guests = booking.Guests,
                Room_id = GetIdFromHref(booking.RoomHref),
                Room_type_id = GetIdFromHref(booking.RoomTypeHref),
                Customer_id = GetIdFromHref(booking.CustomerHref),
            };
        }

        public static BookingDto Map(this BookingEntity booking)
        {
            if (booking == null) return null;
            return new BookingDto()
            {
                Href = booking.ExtractHref(),
                StartDate = booking.Start_date,
                EndDate = booking.End_date,
                Guests = booking.Guests,
                RoomHref = GetHrefFromId(typeof(RoomEntity), booking.Room_id),
                RoomTypeHref = GetHrefFromId(typeof(RoomTypeEntity), booking.Room_type_id),
                CustomerHref = GetHrefFromId(typeof(CustomerEntity), booking.Customer_id),
            };
        }

        public static RoomTypeDto Map(this RoomTypeEntity room)
        {
            if (room == null) return null;
            return new RoomTypeDto()
            {
                Href = room.ExtractHref(),
                Type = room.Type,
                Avaliable = room.Avaliable,
                Beds = room.beds,
                Description = room.Description,
                Rating = room.Rating,
                hotelHref = GetHrefFromId(typeof(HotelEntity), room.Hotel_Id),
                Active = room.Active ?? false,
            };
        }

        public static RoomTypeEntity Map(this RoomTypeDto room)
        {
            if (room == null) return null;
            return new RoomTypeEntity()
            {
                Id = room.ExtractId(),
                Type = room.Type,
                Avaliable = room.Avaliable,
                beds = room.Beds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = GetIdFromHref(room.hotelHref),
                Active = room.Active,
            };
        }

        public static LocationDto Map(this LocationEntity location)
        {
            return new LocationDto()
            {
                Href = location.ExtractHref(),
                StreetAddress = location.Street_Address,
                City = location.City,
                Country = location.Country,
                ZipCode = location.Zip_code,
                Lat = location.Lat,
                Lng = location.Lng,
            };
        }
        public static PictureDto Map(this PictureEntity picture)
        {
            return new PictureDto()
            {
                Href = picture.ExtractHref(),
                HotelHref = picture.Type.Equals("hotel") ? GetHrefFromId(typeof(HotelEntity), picture.Hotel_id) : null,
                RoomHref = picture.Type.Equals("room") ? GetHrefFromId(typeof(RoomEntity), picture.Room_id) : null,
                Path = picture.Path,
                Description = picture.Description,
                Title = picture.Title

            };
        }
        public static RoomDto Map(this RoomEntity room)
        {
            return new RoomDto()
            {
                Href = room.ExtractHref(),
                RoomTypeHref = GetHrefFromId(typeof(RoomTypeEntity), room.Room_type_id),
                Room_number = room.Room_number,
                Notes = room.Notes,
                Active = room.Active ?? false,
            };
        }

        public static RoomEntity Map(this RoomDto room)
        {
            return new RoomEntity()
            {
                Id = room.ExtractId(),
                Room_type_id = GetIdFromHref(room.RoomTypeHref),
                Room_number = room.Room_number,
                Notes = room.Notes,
                Active = room.Active,
            };
        }
        public static CustomerEntity Map(this CustomerDto customer)
        {
            return new CustomerEntity()
            {
                Id = customer.ExtractId(),
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Birthday = customer.Birthday,

            };
        }
        public static CustomerDto Map(this CustomerEntity customer)
        {
            return new CustomerDto()
            {
                Href = customer.ExtractHref(),
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Birthday = customer.Birthday,
            };
        }


        public static PriceEntity Map(this PriceDto price)
        {
            return new PriceEntity()
            {
                Id = price.ExtractId(),
                Value = price.Value,
                Room_Type_Id = price.RoomTypeId,
                Start_Date = price.StartDate,

            };
        }
        public static PriceDto Map(this PriceEntity price)
        {
            return new PriceDto()
            {
                Href = price.ExtractHref(),
                Value = price.Value,
                RoomTypeId = price.Room_Type_Id,
                StartDate = price.Start_Date,
            };
        }

        #region helperMethods
        public static int? ExtractId(this BaseModelDto dto)
        {
            return GetIdFromHref(dto.Href);
        }

        public static string? ExtractHref(this BaseEntity model)
        {
            return GetHrefFromId(model.GetType(), model.Id);
        }

        public static string? GetHrefFromId(Type type, int? id)
        {
            if (id is null) return null;
            string typeName = type.Name.Substring(0, type.Name.IndexOf("Entity"));
            return $@"api/{typeName}/{id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;
            int idIndex = href.LastIndexOf("/") + 1;
            bool res = int.TryParse(href[idIndex..], out int result);
            return res ? result : throw new HrefFormatException($"Parse error: could not find ID in Href at {idIndex}");
        }
        #endregion
    }
}
