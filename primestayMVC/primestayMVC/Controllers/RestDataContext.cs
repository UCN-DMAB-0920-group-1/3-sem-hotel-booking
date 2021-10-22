using MVC.DataAccessLayer;
using RestSharp;

namespace primestayMVC.Controllers
{
    public class RestDataContext : IDataContext<IRestClient>
    {
        private static readonly string _baseUrl = "https://localhost:44312";

        public IRestClient Open()
        {
            return new RestClient(_baseUrl);
        }
    }
}
