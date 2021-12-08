using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        #region setup
        private readonly IDao<CustomerEntity> _dao;
        public CustomerController(IDao<CustomerEntity> dao)
        {
            _dao = dao;
        }
        #endregion

        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomerDto>> Index([FromQuery] CustomerDto customer)
        {
            var customers = _dao.ReadAll(customer.Map()).Select(x => x.Map());
            return customers.Any() ? Ok(customers) : NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CustomerDto> Details(int id)
        {
            var customer = _dao.ReadById(id).Map();
            return customer is not null ? Ok(customer) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CustomerDto> Create([FromBody] CustomerDto customer)
        {
            var res = _dao.Create(customer.Map());
            customer.Href = "api/customer/" + res;
            if (res > 0) return Created(customer.Href, customer);
            else return BadRequest(res);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CustomerDto> Edit([FromBody] CustomerDto customer)
        {
            return _dao.Update(customer.Map()) switch
            {
                > 0 => Ok(customer),
                -1 => BadRequest(),
                _ => throw new Exception("something went wrong")
            };
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CustomerDto> Delete(CustomerDto customer)
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
