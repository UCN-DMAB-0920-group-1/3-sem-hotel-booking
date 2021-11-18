using PrimestayWpf.Model;

namespace PrimeStay.WPF.DataAccessLayer.DTO
{
    public class HotelDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StaffedHours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }

        public static implicit operator Hotel(HotelDto hotel)
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
        public static implicit operator HotelDto(Hotel hotel)
        {
            return new Hotel()
            {
                Name = hotel.Name,
                Description = hotel.Description,
                StaffedHours = hotel.StaffedHours,
                LocationHref = hotel.LocationHref,
                Stars = hotel.Stars,
                href = hotel.href,

            };
        }
    }
}
