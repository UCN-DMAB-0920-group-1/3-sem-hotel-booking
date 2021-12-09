using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        #region setup
        private readonly IDao<PriceEntity> _dao;

        public PriceController(IDao<PriceEntity> priceDao)
        {
            _dao = priceDao;
        }
        #endregion

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PriceDto>> Index([FromQuery] PriceDto price)
        {
            try
            {
                var res = _dao.ReadAll(price.Map()).Select(p => p.Map()).ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PriceDto> Details([FromQuery] int id)
        {
            try
            {
                return Ok(_dao.ReadById(id).Map());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PriceDto> Create([FromBody] PriceDto price)
        {
            try
            {
                var priceId = _dao.Create(price.Map());
                price.Href = $"api/price/{priceId}";
                return Created(price.Href, price);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
