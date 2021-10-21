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
                LocationHref = @$"api/Location/{hotel.Location_Id}" // TODO use helper method GetHrefFromId()
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

        public static int? ExtractId(this BaseModel dto)
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

            return $@"api/{type.Name.ToLower()}/{id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (string.IsNullOrEmpty(href)) return null;

            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
    }
}
