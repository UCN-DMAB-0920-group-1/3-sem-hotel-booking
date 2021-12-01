using System;

/**
* Author: Lars Nysom
*/
namespace API.Services.Models
{
    public enum UserType
    {
        Customer,
        Staff,
        Admin,
    }
    public class Userinfo
    {
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
        public int? Id { get; set; }
        public UserType UserType { get; set; }
    }
}
