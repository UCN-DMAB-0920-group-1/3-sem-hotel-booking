using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IDao<BookingEntity> _dao;
        public BookingController(IDao<BookingEntity> dao)
        {
            _dao = dao;
        }
        // GET: BookingController
        [HttpGet]
        public IEnumerable<BookingDto> Index(int? id, DateTime? start_date, DateTime? end_date, int? num_of_guests, int? room_id, int? customer_id)
            => _dao.ReadAll(new BookingEntity()
            {
                Id = id,
                Start_date = start_date,
                End_date = end_date,
                Num_of_guests = num_of_guests,
                Room_id = room_id,
                Customer_id = customer_id

            }).Select(h => h.Map());

        // GET: BookingController/Details/5
        [HttpGet]
        [Route("{id}")]
        public BookingDto Details(int id)
        {
            return _dao.ReadById(id).Map();
        }

        // POST: BookingController/
        [HttpPost]
        public ActionResult Create([FromBody] BookingDto booking)
        {
            int id = _dao.Create(booking.Map());
            booking.Href = $"api/booking/{id}";
            return Created(booking.Href, booking);
        }

        // PUT: BookingController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            throw new NotImplementedException();
        }

        // DELETE: BookingController/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            throw new NotImplementedException();
        }
    }
}
