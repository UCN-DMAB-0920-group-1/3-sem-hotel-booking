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

        public IActionResult Info([FromQuery] string href)
        {
            HttpContext.Session.SetString("selectedRoom", href);
            return View();
        }
        public IActionResult Create(IFormCollection collection)
        {

            

            try
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
                    Start_date = null,
                    End_date = null,
                    Num_of_guests 
                    
                };

                _dao.Create()

                return View("confirm");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
