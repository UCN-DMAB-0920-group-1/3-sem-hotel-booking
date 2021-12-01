using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IDao<PriceEntity> _dao;

        public PriceController(IDao<PriceEntity> bookingDao)
        {
            _dao = bookingDao;
 
        }

        [HttpGet]
        public ActionResult<IEnumerable<PriceDto>> Index([FromQuery] PriceDto price)
        {
            try
            {
                var res = _dao.ReadAll(price.Map()).Select(h => h.Map());
                return Ok(res);

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        public ActionResult<PriceDto> details([FromQuery] int id)
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
        public ActionResult<int> create([FromQuery] PriceDto price)
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
