using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using WebClient.Service;

namespace WebClient.Controllers
{
    public class BookingController : Controller
    {
        private readonly IDao<BookingDto> _bookingDao;
        public BookingController(IDao<BookingDto> bookingDao)
        {
            _bookingDao = bookingDao;

        }

        public IActionResult Info()
        {
            string jwt = Request.Cookies["jwt"];
            return JwtMethods.HasToken(jwt) ? Create(jwt) : View("../Customer/Login");
        }
        public IActionResult Create(string jwt)
        {
            string customerIdAsString = JwtMethods.GetCustomerIdFromJwtToken(jwt);
            Booking booking = new()
            {
                StartDate = DateTime.Parse(Request.Query["startDate"] + "Z"),
                EndDate = DateTime.Parse(Request.Query["endDate"] + "Z"),
                Guests = int.Parse(Request.Query["guests"]),
                RoomTypeHref = Request.Query["roomType"],
                CustomerId = int.Parse(customerIdAsString),

            };

            string href = _bookingDao.Create(booking.Map());
            if (href is null || href.EndsWith("-1")) return View("BookingError");

            return View("confirm", _bookingDao.ReadByHref(href).Map());
        }


    }
}
