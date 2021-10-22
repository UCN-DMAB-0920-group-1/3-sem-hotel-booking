using PrimeStay.Model;
using System;

namespace PrimeStay.DataAccessLayer.DAO.DTO
{
    internal static class DtoMapper
    {
        public static HotelDto Map(this HotelDal hotel)
        {
            if (hotel == null) return null;
            return new HotelDto()
            {
                Href = hotel.ExtractHref(),
                Name = hotel.Name,
                Description = hotel.Description,
                StaffedHours = hotel.Staffed_hours,
                Stars = hotel.Stars,
                LocationHref = GetHrefFromId(typeof(LocationDal), hotel.Location_Id)
            };
        }

        public static HotelDal Map(this HotelDto hotel)
        {
            if (hotel == null) return null;
            return new HotelDal()
            {
                Id = hotel.ExtractId(),
                Name = hotel.Name,
                Description = hotel.Description,
                Staffed_hours = hotel.StaffedHours,
                Stars = hotel.Stars,
                Location_Id = GetIdFromHref(hotel.LocationHref)
            };
        }

        public static int? ExtractId(this BaseDto dto)
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
            string typeName = type.Name.Substring(0, (type.Name.Length - 3));

            return $@"api/{typeName}/{id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;

            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
    }
}
