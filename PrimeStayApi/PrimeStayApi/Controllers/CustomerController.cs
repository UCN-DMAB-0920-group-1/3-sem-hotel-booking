using Microsoft.AspNetCore.Authorization;
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
    public class CustomerController : Controller
    {
        private readonly IDao<CustomerEntity> _dao;
        public CustomerController(IDao<CustomerEntity> dao)
        {
            _dao = dao;
        }
        // GET: BookingController

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<CustomerDto> Index([FromQuery] CustomerDto customer)
        {
            return _dao.ReadAll(customer.Map()).Select(x => x.Map());
        }

        // GET: api/Booking/5
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        public CustomerDto Details(int id)
        {
            return _dao.ReadById(id).Map();
        }

        // POST: BookingController/
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create([FromBody] CustomerDto customer)
        {
            var res = _dao.Create(customer.Map());
            if (res > 0) return Created("api/customer/" + res, customer);
            else return BadRequest(res);
        }

        // PUT: BookingController/Edit/5
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            throw new NotImplementedException();
        }

        // DELETE: BookingController/Delete/5
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            throw new NotImplementedException();
        }
    }
}
