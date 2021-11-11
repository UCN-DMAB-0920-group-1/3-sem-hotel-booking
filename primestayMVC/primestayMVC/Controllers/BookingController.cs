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

        public IActionResult Info()
        {
            return View();
        }
        public IActionResult Create(IFormCollection collection, string hotelHref, string startDate, string endDate, string guests, string roomType)
        {

            Booking booking = new()
            {
                Start_date = DateTime.Parse(startDate + "Z"),
                End_date = DateTime.Parse(endDate + "Z"),
                Guests = int.Parse(guests),
                Room_type_href = roomType,
                Customer_href = "api/cutomer/1", //TODO find actual customer ;-) 


            };

            string href = _dao.Create(booking.Map());

            if (href.EndsWith("-1")) return View("BookingError");

            return View("confirm", _dao.ReadByHref(href).Map());


        }

    }
}
