using PrimeStay.WPF.DataAccessLayer.DAO;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimestayWPF.DataAccessLayer
{
    internal static class AuthenticationExtension
    {
        public static IRestRequest AddAuthorization(this IRestRequest request, string token)
        {
            request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }
    }
}
