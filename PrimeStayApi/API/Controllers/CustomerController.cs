using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class CustomerController : Controller
    {
        private readonly IDao<CustomerEntity> _dao;
        public CustomerController(IDao<CustomerEntity> dao)
        {
            _dao = dao;
        }

        // GET: BookingController
        [HttpGet]
        public IEnumerable<CustomerDto> Index([FromQuery] CustomerDto customer)
        {
            return _dao.ReadAll(customer.Map()).Select(x => x.Map());
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
            var res = _dao.Create(customer.Map());
            if (res > 0) return Created("api/customer/" + res, customer);
            else return BadRequest(res);
        }

        // PUT: BookingController/Edit/5
        [HttpPut]
        public ActionResult Edit(CustomerDto customer)
        {
            return _dao.Update(customer.Map()) switch
            {
                > 0 => Ok(customer),
                -1 => BadRequest(),
                _ => throw new Exception("something went wrong")
            };
        }

        // DELETE: BookingController/Delete/5
        [HttpDelete]
        public ActionResult Delete(CustomerDto customer)
        {
            return _dao.Delete(customer.Map()) switch
            {
                > 0 => Ok(customer),
                -1 => BadRequest(),
                _ => throw new Exception("something went wrong")
            };
        }
    }
}
