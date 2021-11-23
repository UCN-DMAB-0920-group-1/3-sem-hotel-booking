using PrimeStay.WPF.DataAccessLayer.DTO;
using PrimestayWpf.Model;
using System;

namespace PrimestayWPF.DataAccessLayer.DTO
{
    public static class Mapper
    {
        public static Hotel Map(this HotelDto hotel)
        {
            return new Hotel()
            {
                Name = hotel.Name,
                Description = hotel.Description,
                StaffedHours = hotel.StaffedHours,
                LocationHref = hotel.LocationHref,
                Stars = hotel.Stars,
                Href = hotel.Href,
                Active = hotel.Active,

            };
        }
        public static HotelDto Map(this Hotel hotel)
        {
            return new HotelDto()
            {
                Name = hotel.Name,
                Description = hotel.Description,
                StaffedHours = hotel.StaffedHours,
                LocationHref = hotel.LocationHref,
                Stars = hotel.Stars,
                Href = hotel.Href,
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
                HotelHref = roomType.HotelHref,
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
                HotelHref = roomType.HotelHref,
                Active = roomType.Active,
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
