﻿using API.Models;
using API.Services;
using API.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

/**
 * Author: Lars Nysom
 */
namespace API.Controllers
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
            try
            {
                var user = _accountService.Authenticate(login.Username, login.Password);

                if (user is null) return StatusCode(StatusCodes.Status500InternalServerError, "Incorrect username/password");
                if (!user.IsAuthenticated) return Unauthorized();

                var response = new LoginResponse
                {
                    Token = user.Token,
                    Expires = user.Expires,
                   
                };
                return Ok(response);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something bad happended");
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] LoginRequest login)
        {
            try
            {
                Userinfo user = _accountService.Save(login.Username, login.Password, "user");
                if (user is not null && user.IsAuthenticated)
                {
                    var response = new LoginResponse
                    {
                        Token = user.Token,
                        Expires = user.Expires,
                    };
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "User not authenticated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User could not be registered");
            }

        }

        [HttpPost]
        [Route("register-admin")]
        public IActionResult RegisterAdmin([FromBody] LoginRequest login)
        {
            try
            {
                Userinfo user = _accountService.Save(login.Username, login.Password, "admin");
                if (user is not null && user.IsAuthenticated)
                {
                    var response = new LoginResponse
                    {
                        Token = user.Token,
                        Expires = user.Expires,
                    };
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "User not authenticated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User could not be registered");
            }
        }
    }
}
