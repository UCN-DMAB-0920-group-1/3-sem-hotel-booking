using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;

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
            bool loggedIn = int.TryParse(Request.Cookies["customerId"], out int customer_id);
            if (loggedIn && customer_id > 0) return Create(customer_id);
            return View("../Customer/Login");
        }
        public IActionResult Create(int customer_id)
        {
            Booking booking = new()
            {
                StartDate = DateTime.Parse(Request.Query["startDate"] + "Z"),
                EndDate = DateTime.Parse(Request.Query["endDate"] + "Z"),
                Guests = int.Parse(Request.Query["guests"]),
                RoomTypeHref = Request.Query["roomType"],
                CustomerId = customer_id,

            };

            string href = _bookingDao.Create(booking.Map());
            if (href is null || href.EndsWith("-1")) return View("BookingError");

            return View("confirm", _bookingDao.ReadByHref(href).Map());
        }


    }
}
