using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IDao<PriceEntity> _dao;

        public PriceController(IDao<PriceEntity> priceDao)
        {
            _dao = priceDao;
        }

        [HttpGet]
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
        public ActionResult<int> Create([FromBody] PriceDto price)
        {
            try
            {
                return Ok(_dao.Create(price.Map()));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
