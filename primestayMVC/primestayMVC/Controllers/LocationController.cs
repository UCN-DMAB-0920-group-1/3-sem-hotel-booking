using Microsoft.AspNetCore.Mvc;
using MVC.DataAccessLayer;
using MVC.DataAccessLayer.DTO;
using primestayMVC.Model;

namespace primestayMVC.Controllers
{
    public class LocationController : Controller
    {
        private readonly IDao<LocationDto> _dao;

        public LocationController(IDao<LocationDto> dao)
        {
            _dao = dao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public Location GetLocationById(int id)
        {
            return _dao.ReadById(id).Map();
        }
    }
}
