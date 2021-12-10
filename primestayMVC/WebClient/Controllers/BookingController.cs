using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class BookingController : Controller
    {
        private readonly IDao<BookingDto> _bookingDao;
        private readonly IDao<HotelDto> _hotelDao;
        private readonly IDao<RoomTypeDto> _roomTypeDao;
        private readonly IDao<LocationDto> _LocationDao;

        public BookingController(IDao<BookingDto> bookingDao, IDao<HotelDto> hotelDao, IDao<RoomTypeDto> roomTypeDao, IDao<LocationDto> locationDao)
        {
            _bookingDao = bookingDao;
            _hotelDao = hotelDao;
            _roomTypeDao = roomTypeDao;
            _LocationDao = locationDao;
        }

        public IActionResult Info()
        {
            string jwt = HttpContext.Session.GetString("Jwt");
            return JwtMethods.HasToken(jwt) ? Create(jwt) : View("../Account/Login");
        }
        public IActionResult Create(string jwt)
        {
            string customerIdAsString = JwtMethods.GetCustomerIdFromJwtToken(jwt);
            string roomTypeHref = Request.Query["roomtype"];
            Booking booking = new()
            {
                StartDate = DateTime.Parse(Request.Query["startDate"] + "Z"),
                EndDate = DateTime.Parse(Request.Query["endDate"] + "Z"),
                Guests = int.Parse(Request.Query["guests"]),
                RoomTypeId = MapperExtension.GetIdFromHref(roomTypeHref),
                CustomerId = int.Parse(customerIdAsString)
            };

            string href = _bookingDao.Create(booking.Map());
            if (href is null || href.EndsWith("-1")) return View("BookingError");

            var bookingInfo = GetAllBookingInfo(href);
            return View("Details", bookingInfo);
        }

        public IActionResult Details(string bookingHref)
        {
            var bookingInfo = GetAllBookingInfo(bookingHref);

            return View(bookingInfo);
        }

        #region Helper methods
        private (Booking, RoomType, Hotel) GetAllBookingInfo(string bookingHref)
        {
            var booking = _bookingDao.ReadByHref(bookingHref).Map();
            var roomTypeDTO = _roomTypeDao.ReadByHref($"api/roomType/{booking.RoomTypeId}");
            var roomType = roomTypeDTO.Map();
            var hotel = _hotelDao.ReadByHref(roomTypeDTO.HotelHref).Map();
            hotel.Location = _LocationDao.ReadByHref($"/api/location/{hotel.LocationId}").Map();
            return (booking, roomType, hotel);

        }
        #endregion
    }
}
