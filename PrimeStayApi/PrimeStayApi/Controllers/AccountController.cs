using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.Models;
using PrimeStayApi.Services;

/**
 * Author: Lars Nysom
 */
namespace PrimeStayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login(LoginRequest login)
        {
            var user = _accountService.Authenticate(login.Username, login.Password);
            if (user.IsAuthenticated)
            {
                var response = new LoginResponse
                {
                    Token = user.Token,
                    Expires = user.Expires,
                };
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}
