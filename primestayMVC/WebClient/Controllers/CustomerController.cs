using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using System.Linq;
using WebClient.Service;

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

            HttpContext.Session.SetString("Email", "Test@test.dk");
            Debug.WriteLine(HttpContext.Session.GetString("Email"));
            return View();
        }
        public IActionResult BookingHistory()
        {
            string token = HttpContext.Session.GetString("Jwt");
            if (!JwtMethods.HasToken(token)) return View("../Account/login");

            int customerId = int.Parse(JwtMethods.GetCustomerIdFromJwtToken(token));
            var bookings = _bookingDao.ReadAll(new Booking()
            {
                CustomerId = customerId,
            }
            .Map()).Select(b => b.Map());

            var customer = _customerDao.ReadByHref($"api/Customer/{customerId}").Map();
            return View((bookings, customer));
        }

    }
}
