using PrimeStay.WPF.DataAccessLayer.DTO;
using PrimestayWpf.Model;

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
                Type = roomType.Type,
                Description = roomType.Description,
                Beds = roomType.Beds,
                Rating = roomType.Rating,
                HotelHref = roomType.HotelHref,

            };
        }
        public static RoomTypeDto Map(this RoomType roomType)
        {
            return new RoomTypeDto()
            {
                Type = roomType.Type,
                Description = roomType.Description,
                Beds = roomType.Beds,
                Rating = roomType.Rating,
                HotelHref = roomType.HotelHref,
            };
        }

    }
}
