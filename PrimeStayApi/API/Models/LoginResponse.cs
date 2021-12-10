using System;

/**
 * Author: Lars Nysom
 */
namespace API.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expires { get; internal set; }
    }
}
