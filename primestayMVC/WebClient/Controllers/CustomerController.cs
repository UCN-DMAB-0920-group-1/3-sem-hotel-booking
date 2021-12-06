using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
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
            return View();
        }
        public IActionResult BookingHistory()
        {
            string token = Request.Cookies["jwt"];
            if (!JwtMethods.HasToken(token)) return View("Login");

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
