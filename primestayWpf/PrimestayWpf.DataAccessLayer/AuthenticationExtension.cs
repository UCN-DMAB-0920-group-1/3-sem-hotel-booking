using RestSharp;

namespace DataAccessLayer
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
