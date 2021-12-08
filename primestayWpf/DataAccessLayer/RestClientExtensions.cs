using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataAccessLayer
{
    internal static class RestClientExtensions
    {
        public static IRestRequest AddAuthorization(this IRestRequest request, string token)
        {
            request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }

        public static IRestRequest AddQueryParametersFromObject(this IRestRequest request, object model)
        {
            Type modelType = model.GetType();
            PropertyInfo[] modelProps = modelType.GetProperties();

            foreach (var property in modelProps)
            {
                var value = property.GetValue(model);
                if (value != null)
                {
                    request.AddQueryParameter(property.Name, value.ToString());
                }
            }

            return request;
        }
    }
}
