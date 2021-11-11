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
                Customer_href = "api/cutomer/1", //TODO find actual customer ;-) 


            };

            string href = _dao.Create(booking.Map());

            if (href.EndsWith("-1")) return View("BookingError");

            return View("confirm", _dao.ReadByHref(href).Map());


        }

    }
}
