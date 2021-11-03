using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System;

namespace PrimeStay.MVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IDao<BookingDto> _dao;
        public BookingController(IDao<BookingDto> dao)
        {
            _dao = dao;

        }

        public IActionResult Info([FromQuery] string href)
        {
            HttpContext.Session.SetString("selectedRoom", href);
            return View();
        }
        public IActionResult Create(IFormCollection collection)
        {
            /*
             * TODO:
             *  Customer = new Customer()
             *  {
             *       Name = collection["Customer.Name"],
             *       Email = collection["Customer.Email"],
             *       Phone = collection["Customer.Phone"],
             *  }
             */
            Booking booking = new()
            {
                Start_date = DateTime.Parse(HttpContext.Session.GetString("checkIn") + "Z"),
                End_date = DateTime.Parse(HttpContext.Session.GetString("checkOut") + "Z"),
                Num_of_guests = int.Parse(HttpContext.Session.GetString("guests")),
                Room_href = HttpContext.Session.GetString("selectedRoom"),
                Customer_href = "api/cutomer/1", //TODO find actual customer ;-) 


            };

            string href = _dao.Create(booking.Map());
            var test = _dao.ReadByHref(href);
            var newBooking = test.Map();
            return View("confirm", newBooking);


        }

    }
}
