using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;

namespace WebClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IDao<BookingDto> _bookingDao;
        private readonly IDao<CustomerDto> _customerDao;
        public CustomerController(IDao<BookingDto> bookingDao, IDao<CustomerDto> customerDao)
        {
            _bookingDao = bookingDao;
            _customerDao = customerDao;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult BookingHistory()
        {
            bool loggedIn = int.TryParse(Request.Cookies["customerId"], out int customer_id);
            //Send to  "you have to log in to view this page" page
            if (!loggedIn || customer_id < 1) return View("Login");
            var bookings = _bookingDao.ReadAll(new Booking() { CustomerId = customer_id }.Map()).Select(b => b.Map());
            var customer = _customerDao.ReadByHref($"api/Customer/{customer_id}").Map();
            return View((bookings, customer));

        }

    }
}
