using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.Models;
using PrimeStayApi.Services;
using PrimeStayApi.Services.Models;
using System;

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
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest login)
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

        [HttpPost]
        [Route("register-admin")]
        public IActionResult RegisterAdmin([FromBody] LoginRequest login)
        {
            try
            {
                Userinfo user = _accountService.Save(login.Username, login.Password);
                if (user.IsAuthenticated)
                {
                    var response = new LoginResponse
                    {
                        Token = user.Token,
                        Expires = user.Expires,
                    };
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Register failed");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Register failed");
            }
        }
    }
}
