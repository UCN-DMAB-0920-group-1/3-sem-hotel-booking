using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using WebClient.Models;

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
            return View();
        }
        public IActionResult Create()
        {
            Customer customer = new()
            {
                Name = Request.Form["name"],
                Email = Request.Form["email"],
                Phone = Request.Form["phone"],
                Birthday = DateTime.Parse(Request.Form["Birthday"] + "Z"),
            };
            Booking booking = new()
            {
                StartDate = DateTime.Parse(Request.Query["startDate"] + "Z"),
                EndDate = DateTime.Parse(Request.Query["endDate"] + "Z"),
                Guests = int.Parse(Request.Query["guests"]),
                RoomTypeHref = Request.Query["roomType"],
                Customer = customer,


            };

            string href = _bookingDao.Create(booking.Map());
            if (href is null || href.EndsWith("-1")) return View("BookingError");

            return View("confirm", _bookingDao.ReadByHref(href).Map());
        }

    }
}
