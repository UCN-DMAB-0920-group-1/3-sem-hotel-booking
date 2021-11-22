using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using System;
using System.Collections.Generic;

namespace PrimeStayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IDao<CustomerEntity> _dao;
        public CustomerController(IDao<CustomerEntity> dao)
        {
            _dao = dao;
        }
        // GET: BookingController
        [HttpGet]
        public IEnumerable<CustomerEntity> Index([FromQuery] CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        // GET: api/Booking/5
        [HttpGet]
        [Route("{id}")]
        public CustomerDto Details(int id)
        {
            return _dao.ReadById(id).Map();
        }

        // POST: BookingController/
        [HttpPost]
        public ActionResult Create([FromBody] CustomerDto customer)
        {
            throw new NotImplementedException();
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
