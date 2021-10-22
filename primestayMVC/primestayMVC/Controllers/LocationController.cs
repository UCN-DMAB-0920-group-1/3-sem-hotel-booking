using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using primestayMVC.Model;
using PrimeStay.Model;
using PrimeStay.DataAccessLayer;

namespace primestayMVC.Controllers
{
    public class LocationController : Controller
    {
        private readonly IDao<LocationDal> _dao;

        public LocationController(IDao<LocationDal> dao)
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
