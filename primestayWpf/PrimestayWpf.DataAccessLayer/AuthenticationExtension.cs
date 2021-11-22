using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimestayWPF.DataAccessLayer
{
    internal static class AuthenticationExtension
    {
        public static IRestRequest AddAuthentication(this IRestRequest request, string token)
        {
            request.AddHeader("Authentication", $"Bearer {token}");
            return request;
        }
    }
}
