using Microsoft.AspNetCore.Mvc;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System;

namespace PrimeStay.MVC.Controllers
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
            };
            Booking booking = new()
            {
                Start_date = DateTime.Parse(Request.Query["startDate"] + "Z"),
                End_date = DateTime.Parse(Request.Query["endDate"] + "Z"),
                Guests = int.Parse(Request.Query["guests"]),
                Room_type_href = Request.Query["roomType"],
                Customer = customer,


            };

            string href = _bookingDao.Create(booking.Map());

            if (href.EndsWith("-1")) return View("BookingError");

            return View("confirm", _bookingDao.ReadByHref(href).Map());


        }

    }
}
