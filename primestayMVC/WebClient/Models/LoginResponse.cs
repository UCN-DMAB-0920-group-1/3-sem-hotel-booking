using System;

namespace WebClient.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expires { get; internal set; }
        public int? Userid { get; set; }
    }

}
