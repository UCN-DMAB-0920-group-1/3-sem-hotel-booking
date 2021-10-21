using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeStay.DataAccessLayer;
using PrimeStay.Model;
using primestayMVC.Model;
using primestayMVC.Models;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace primestayMVC.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IDao<Hotel> _dao;

        public HotelController(ILogger<HotelController> logger, IDao<Hotel> dao)
        {
            _logger = logger;
            _dao = dao;
        }
        public IActionResult Index([FromQuery] Hotel hotel)
        {
            //if (hotels == null) 
            IEnumerable<Hotel> hotels = _dao.ReadAll(hotel);
            hotels.Select(h => h.Map()).ToList().ForEach(h => h.Location = getHotelLocation(h));

            return View(hotels);
        }


        //[Route("Details")]
        public IActionResult Details(string href)
        {
            var hotel = GetHotel(href);
            hotel.rooms = RoomController.getAllHotelRooms(href);
            return View(hotel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private Hotel GetHotel(string href)
        {
            RestClient client = new("https://localhost:44312/");
            RestRequest request = new(href, Method.GET, DataFormat.Json);
            return client.Execute<Hotel>(request).Data;

        }
        public static IEnumerable<Hotel> GetAllHotels()
        {

            RestClient client = new("https://localhost:44312/");
            RestRequest request = new("api/hotel/", Method.GET, DataFormat.Json);
            IRestResponse<IEnumerable<Hotel>> restResponse = client.Get<IEnumerable<Hotel>>(request);
            return restResponse.Data;
        }
        private Location getHotelLocation(Hotel h) => LocationController.GetLocation(h.LocationHref);

    }
}
