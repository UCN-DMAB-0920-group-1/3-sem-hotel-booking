using DataAccessLayer;
using RestSharp;

namespace WinApp
{
    public class RestDataContext : IDataContext<IRestClient>
    {
        private const string _baseUrl = "https://localhost:44312";

        #region Singleton
        private RestDataContext() { }
        private static RestDataContext? _instance;
        public static RestDataContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RestDataContext();
            }
            return _instance;
        }

        #endregion

        public IRestClient Open()
        {
            return new RestClient(_baseUrl);
        }
    }
}
