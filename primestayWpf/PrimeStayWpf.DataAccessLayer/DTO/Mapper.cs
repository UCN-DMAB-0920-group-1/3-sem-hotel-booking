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
                href = hotel.Href,

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
                Href = hotel.href,

            };
        }

    }
}
