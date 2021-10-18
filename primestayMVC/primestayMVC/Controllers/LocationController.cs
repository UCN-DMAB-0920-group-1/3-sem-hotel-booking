using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using primestayMVC.Model;

namespace primestayMVC.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public static Location GetLocation(string href)
        {
            RestClient client = new("https://localhost:44312/");
            RestRequest request = new(href, Method.GET, DataFormat.Json);
            return client.Execute<Location>(request).Data;

        }
    }
}
