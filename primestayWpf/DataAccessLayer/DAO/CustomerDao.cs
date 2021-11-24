using DataAccessLayer.DTO;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace DataAccessLayer.DAO

{
    internal class CustomerDao : BaseDao<IDataContext<IRestClient>>, IDao<CustomerDto>
    {
        private readonly string baseEndPoint = "/api/customer";
        public CustomerDao(IDataContext<IRestClient> dataContext, string token) : base(dataContext, token)
        {
        }

        public string Create(CustomerDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/customer/", Method.POST, DataFormat.Json);

            var res = restClient.Post<CustomerDto>(restRequest).Data;
            return res.Href;
        }

        public int Delete(CustomerDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.DELETE, DataFormat.Json);
            restRequest.AddAuthorization(AccessToken);
            restRequest.AddJsonBody(model);

            var response = restClient.Delete(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.OK => 1,
                _ => -1
            };
        }

        public IEnumerable<CustomerDto> ReadAll(CustomerDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/customer/", Method.GET, DataFormat.Json);
            restRequest.AddAuthorization(AccessToken);

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

        public int Update(CustomerDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/customer/", Method.PUT, DataFormat.Json);
            restRequest.AddAuthorization(AccessToken);
            restRequest.AddJsonBody(model);
            var response = restClient.Execute(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.OK => 1,
                _ => -1
            };
        }
    }
}