using PrimeStay.DataAccessLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primestayWpf
{
    internal class RestDataContext : IDataContext<IRestClient>
    {
        private const string _baseUrl = @"https://localhost:44312";

        #region Singleton
        public static RestDataContext Instance
        {
            get
            {
                if (Instance == null) Instance = new RestDataContext();
                return Instance;
            }
            private set
            {
            }
        }

        private RestDataContext() { }
        #endregion

        public IRestClient Open()
        {
            return new RestClient(_baseUrl);
        }
    }
}
