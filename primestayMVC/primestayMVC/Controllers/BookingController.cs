using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using primestay.MVC.Model;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System;
using System.Collections.Generic;
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

        public IActionResult Info()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                Booking booking = new()
                {
                    Start_date = null,
                    End_date = null,
                    Customer = new Customer()
                    {
                        Name = collection["Customer.Name"],
                        Email = collection["Customer.Email"],
                        Phone = collection["Customer.Phone"],
                    }
                };
                return RedirectToAction(nameof(Index), "Hotel");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
