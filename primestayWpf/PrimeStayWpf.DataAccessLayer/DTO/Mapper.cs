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



        public static Customer Map(this CustomerDto customer)
        {
            return new Customer()
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                BirthDay = customer.BirthDay,
            };
        }
        public static CustomerDto Map(this Customer customer)
        {
            return new CustomerDto ()
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                BirthDay = customer.BirthDay,
            };
        }
    }
}
