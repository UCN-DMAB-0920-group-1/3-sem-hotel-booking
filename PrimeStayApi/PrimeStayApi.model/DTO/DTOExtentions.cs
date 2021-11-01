using System;

namespace PrimeStayApi.Model.DTO
{
    public static class DtoExtentions
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
                LocationHref = @$"api/Location/{hotel.Location_Id}" // TODO use helper method GetHrefFromId()
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
                Num_of_guests = booking.NumOfGuests,
                Room_id = GetIdFromHref(booking.RoomHref),
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
                NumOfGuests = booking.Num_of_guests,
                RoomHref = @$"api/Room/{booking.Room_id}",
                CustomerHref = @$"api/Customer/{booking.Customer_id}" // TODO use helper method GetHrefFromId()
            };
        }

        public static RoomDto Map(this RoomEntity room)
        {
            if (room == null) return null;
            return new RoomDto()
            {
                Href = room.ExtractHref(),
                Type = room.Type,
                NumOfAvaliable = room.Num_of_avaliable,
                NumOfBeds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                hotelHref = GetHrefFromId(typeof(HotelEntity), room.Hotel_Id)
            };
        }

        public static RoomEntity Map(this RoomDto room)
        {
            if (room == null) return null;
            return new RoomEntity()
            {
                Id = room.ExtractId(),
                Type = room.Type,
                Num_of_avaliable = room.NumOfAvaliable,
                Num_of_beds = room.NumOfBeds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = GetIdFromHref(room.hotelHref),
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
            };
        }

        public static int? ExtractId(this BaseModelDto dto)
        {
            return GetIdFromHref(dto.Href);
        }

        public static string ExtractHref(this BaseEntity model)
        {

            return GetHrefFromId(model.GetType(), model.Id);
        }

        public static string GetHrefFromId(Type type, int? id)
        {
            if (id == null) return null;
            string typeName = type.Name.Substring(0, type.Name.IndexOf("Entity"));
            return $@"api/{typeName}/{id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;

            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
    }
}
