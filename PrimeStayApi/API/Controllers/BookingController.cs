using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
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
        #region setup
        private readonly IDao<BookingEntity> _dao;
        private readonly IDao<CustomerEntity> _customerDao;

        public BookingController(IDao<BookingEntity> bookingDao, IDao<CustomerEntity> customerDao)
        {
            _dao = bookingDao;
            _customerDao = customerDao;
        }
        #endregion

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BookingDto> Details(int id)
        {
            try
            {
                return Ok(_dao.ReadById(id).Map());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BookingDto> Create(BookingDto booking)
        {

            int id = _dao.Create(booking.Map());

            if (id > 0)
            {
                booking.Href = $"api/booking/{id}";
                return Created(booking.Href, booking);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
