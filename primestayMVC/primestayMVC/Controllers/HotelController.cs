using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using primestayMVC.Model;
using primestayMVC.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace primestayMVC.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;

        public HotelController(ILogger<HotelController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
           var hotels = GetAllHotels();
           
           hotels.ToList().ForEach(h => h.Location = LocationController.GetLocation(h.LocationHref));
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
        public static IEnumerable<Hotel> GetAllHotels() { 
        
            RestClient client = new("https://localhost:44312/");
            RestRequest request = new("api/hotel/", Method.GET, DataFormat.Json);
            return client.Execute<IEnumerable<Hotel>>(request).Data;
        }
    }
}
