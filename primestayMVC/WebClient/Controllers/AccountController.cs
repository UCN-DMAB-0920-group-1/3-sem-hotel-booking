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
            return View("Login",username);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("LoggedIn", "false");
            HttpContext.Session.Clear();
            Debug.WriteLine(HttpContext.Session.GetString("LoggedIn"));
            return View("../Hotel/Index");
        }


        public IActionResult Authorize()
        {
            string username = Request.Form["Username"];
            string password = Request.Form["Password"];

            var res = ((IDaoAccountExtension<LoginResponse>)_accountDao).Authorize(username,password);

            if(res is not null && res.Token is not null) {
                HttpContext.Session.SetString("Jwt", res.Token);
                HttpContext.Session.SetString("LoggedIn", "true");
                HttpContext.Session.SetString("Username", res.Username);
                
            } else
            {
                return RedirectToAction("Login","Account", new { @username = username});
            }
            return Redirect("../Hotel/Index");
        }
    }
}
