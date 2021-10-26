using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using primestay.MVC.Model;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeStay.MVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IDao<BookingDto> _dao;
        private readonly RoomController _roomCTRL;
        public BookingController(IDao<BookingDto> dao, IDao<RoomDto> roomDao)
        {
            _dao = dao;
            _roomCTRL = new RoomController(roomDao);

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
                Num_of_guests = HttpContext.Session.GetInt32("guests"),
                Room_href = HttpContext.Session.GetString("selectedRoom"),
                Customer_href = "api/cutomer/1", //TODO find actual customer ;-) 


            };

            _dao.Create(booking.Map());

            return View("confirm");


        }

    }
}
