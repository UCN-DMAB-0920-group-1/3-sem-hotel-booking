using API.Models;
using API.Services;
using API.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    /**
     * Author: Julius, Magnus, Michael, Mike, Nicolas
     * Origin: Lars Nysom
     * <summary>Class <see cref="AccountController"/> handles http requests related to user login/registration</summary>
     */
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region setup
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest login)
        {
            try
            {
                var user = _accountService.Authenticate(login.Username, login.Password);

                if (user is null) return StatusCode(StatusCodes.Status500InternalServerError, "Incorrect username/password");
                if (!user.IsAuthenticated) return Unauthorized();
                LoginResponse response = CreateResponse(user);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LoginResponse> Register([FromBody] LoginRequest login)
        {
            try
            {
                Userinfo user = _accountService.Save(login.Username, login.Password, "user");
                if (user is not null && user.IsAuthenticated)
                {
                    LoginResponse response = CreateResponse(user);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LoginResponse> RegisterAdmin([FromBody] LoginRequest login)
        {
            try
            {
                Userinfo user = _accountService.Save(login.Username, login.Password, "admin");
                if (user is not null && user.IsAuthenticated)
                {
                    LoginResponse response = CreateResponse(user);
                    return Ok(response);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "User not authenticated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "User could not be registered");
            }
        }

        private static LoginResponse CreateResponse(Userinfo user)
        {
            return new LoginResponse
            {
                Token = user.Token,
                Expires = user.Expires,
                CustomerId = user.CustomerId,
            };
        }
    }
}
