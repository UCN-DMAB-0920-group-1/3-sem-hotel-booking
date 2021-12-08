using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;


namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDao<UserDto> _accountDao;

        public AccountController(IDao<UserDto> accountDao)
        {
            _accountDao = accountDao;
        }
        public IActionResult Login(string username = "", string password = "")
        {
            return View((username,password));
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("LoggedIn", "false");
            Debug.WriteLine(HttpContext.Session.GetString("LoggedIn"));
            return View("../Hotel/Index");
        }


        public IActionResult Authorize()
        {
            var res = ((IDaoAccountExtension<LoginResponse>)_accountDao).Authorize(Request.Form["Username"], Request.Form["Password"]);
            if(res.Token is not null) {
                HttpContext.Session.SetString("Jwt", res.Token);
                HttpContext.Session.SetString("LoggedIn", "true");
                
            } else
            {
                Login(Request.Form["Username"], Request.Form["Username"]);
            }
            return View("../Hotel/Index");
        }
    }
}
