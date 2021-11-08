using System;

/**
* Author: Lars Nysom
*/
namespace PrimeStayApi.Services.Models
{
    public class Userinfo
    {
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
    }
}
