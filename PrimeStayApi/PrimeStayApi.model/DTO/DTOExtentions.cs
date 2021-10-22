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
                Staffed_hours = hotel.Staffed_hours,
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
                Staffed_hours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                Location_Id = GetIdFromHref(hotel.LocationHref) ?? 0
            };
        }

        public static RoomDto Map(this RoomEntity room)
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
                Num_of_avaliable = room.Num_of_avaliable,
                Num_of_beds = room.Num_of_beds,
                Description = room.Description,
                Rating = room.Rating,
                Hotel_Id = GetIdFromHref(room.hotelHref) ?? 0
            };
        }

        public static LocationDto Map(this LocationEntity location)
        {
            return new LocationDto()
            {
                Href = location.ExtractHref(),
                Street_Address = location.Street_Address,
                City = location.City,
                Country = location.Country,
                Zip_code = location.Zip_code,
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

            return $@"api/{type.Name.ToLower()}/{id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;

            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
    }
}
