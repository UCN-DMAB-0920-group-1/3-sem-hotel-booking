using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IDao<BookingEntity> _dao;
        private readonly IDao<CustomerEntity> _customerDao;
        public BookingController(IDao<BookingEntity> bookingDao, IDao<CustomerEntity> customerDao)
        {
            _dao = bookingDao;
            _customerDao = customerDao;
        }
        // GET: BookingController
        [HttpGet]
        public ActionResult<IEnumerable<BookingDto>> Index([FromQuery] BookingDto booking)
        {
            try
            {
                var res = _dao.ReadAll(booking.Map()).Select(h => h.Map());
                return Ok(res);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        // GET: api/Booking/5
        [HttpGet]
        [Route("{id}")]
        public BookingDto Details(int id)
        {
            return _dao.ReadById(id).Map();
        }

        // POST: BookingController/
        [HttpPost]
        public ActionResult Create(BookingDto booking)
        {
            int newCustomerId = 0;
            var matches = _customerDao.ReadAll(new CustomerEntity() { Email = booking.Customer.Email });
            bool isNewCustomer = !matches.Any();

            if (isNewCustomer)
            {
                newCustomerId = _customerDao.Create(booking.Customer.Map());
                booking.CustomerHref = "api/Customer/" + newCustomerId;
            }
            else
            {
                booking.CustomerHref = matches.First().ExtractHref();
            }

            int id = _dao.Create(booking.Map());

            if (id > 0)
            {
                booking.Href = $"api/booking/{id}";
                return Created(booking.Href, booking);
            }
            else
            {
                if (isNewCustomer) _customerDao.Delete(new CustomerEntity() { Id = newCustomerId });
                return BadRequest();
            }
        }

        // PUT: BookingController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingDto booking)
        {
            throw new NotImplementedException();
        }

        // DELETE: BookingController/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BookingDto booking)
        {
            throw new NotImplementedException();
        }
    }
}
