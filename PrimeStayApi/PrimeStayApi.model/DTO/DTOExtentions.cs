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
                Guests = booking.Guests,
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
                Guests = booking.Guests,
                RoomHref = @$"api/Room/{booking.Room_id}",
                CustomerHref = @$"api/Customer/{booking.Customer_id}" // TODO use helper method GetHrefFromId()
            };
        }

        public static RoomDto Map(this RoomTypeEntity room)
        {
            if (room == null) return null;
            return new RoomDto()
            {
                Href = room.ExtractHref(),
                Type = room.Type,
                Avaliable = room.Avaliable,
                NumOfBeds = room.beds,
                Description = room.Description,
                Rating = room.Rating,
                hotelHref = GetHrefFromId(typeof(HotelEntity), room.Hotel_Id)
            };
        }

        public static RoomTypeEntity Map(this RoomDto room)
        {
            if (room == null) return null;
            return new RoomTypeEntity()
            {
                Id = room.ExtractId(),
                Type = room.Type,
                Avaliable = room.Avaliable,
                beds = room.NumOfBeds,
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
        public static PictureDto Map(this PictureEntity picture)
        {
            return new PictureDto()
            {
                Href = picture.ExtractHref(),
                HotelHref = picture.Type == "hotel" ? "api/hotel/" + picture.Hotel_id : null,
                RoomHref = picture.Type == "room" ? "api/room/" + picture.Room_id : null,
                Path = picture.Path,
                Description = picture.Description,
                Title = picture.Title

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
