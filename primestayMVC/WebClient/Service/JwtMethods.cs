using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace WebClient.Service
{
    public static class JwtMethods
    {
        public static string GetCustomerIdFromJwtToken(string jwt)
        {
            if (string.IsNullOrWhiteSpace(jwt)) return null;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var customerIdAsString = token.Payload.Claims.First(x => x.Type.Equals("customerId")).Value;
            return customerIdAsString;
        }
        public static bool HasToken(string jwt)
        {
            return !string.IsNullOrWhiteSpace(jwt) && jwt is not null;
        }

        public static IActionResult ViewIfToken(string jwt, IActionResult success, IActionResult fail)
        {
            if (string.IsNullOrWhiteSpace(jwt)) return fail;
            return success;
        }
    }
}
