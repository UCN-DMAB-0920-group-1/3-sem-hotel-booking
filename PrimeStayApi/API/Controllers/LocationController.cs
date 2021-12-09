using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        #region setup
        private readonly IDao<LocationEntity> _dao;
        public LocationController(IDao<LocationEntity> dao)
        {
            _dao = dao;
        }
        #endregion

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LocationDto> Details(int id)
        {
            var location = _dao.ReadById(id).Map();
            return location is not null ? Ok(location) : NotFound();
        }
    }
}
