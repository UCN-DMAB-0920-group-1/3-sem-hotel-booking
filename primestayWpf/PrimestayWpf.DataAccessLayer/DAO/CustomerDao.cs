using PrimeStay.WPF.DataAccessLayer.DTO;
using PrimestayWPF.DataAccessLayer;
using RestSharp;
using System.Collections.Generic;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    internal class CustomerDao : BaseDao<IDataContext<IRestClient>>, IDao<CustomerDto>
    {
        public CustomerDao(IDataContext<IRestClient> dataContext, string token) : base(dataContext, token)
        {
        }

        public string Create(CustomerDto model, string token)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/customer/", Method.POST, DataFormat.Json);

            var res = restClient.Post<CustomerDto>(restRequest).Data;
            return res.Href;
        }

        public int Delete(CustomerDto model, string token)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CustomerDto> ReadAll(CustomerDto model, string token)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/customer/", Method.GET, DataFormat.Json);
            restRequest.AddAuthorization(token);

            var res = restClient.Get<IEnumerable<CustomerDto>>(restRequest).Data;
            return res;
        }

        public CustomerDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/customer/", Method.GET, DataFormat.Json);
            restRequest.AddAuthorization(AccessToken);

            var res = restClient.Get<CustomerDto>(restRequest).Data;
            return res;
        }

        public int Update(CustomerDto model, string token)
        {
            throw new System.NotImplementedException();
        }
    }
}