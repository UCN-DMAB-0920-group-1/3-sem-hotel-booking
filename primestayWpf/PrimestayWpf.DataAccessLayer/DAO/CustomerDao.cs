using PrimeStay.WPF.DataAccessLayer.DTO;
using PrimestayWPF.DataAccessLayer;
using RestSharp;
using System.Collections.Generic;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    internal class CustomerDao : BaseDao<IDataContext<IRestClient>>, IDao<CustomerDto>, IDaoAuth
    {
        public CustomerDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(CustomerDto model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(CustomerDto model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CustomerDto> ReadAll(CustomerDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/customer/", Method.GET, DataFormat.Json);
            restRequest.AddAuthentication(Token);

            var res = restClient.Get<IEnumerable<CustomerDto>>(restRequest).Data;
            return res;
        }

        public CustomerDto ReadByHref(string href)
        {
            throw new System.NotImplementedException();
        }

        public void SetToken(string token)
        {
            Token = token;
        }

        public int Update(CustomerDto model)
        {
            throw new System.NotImplementedException();
        }
    }
}