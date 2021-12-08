using DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.DAO
{
    internal class CustomerDao : BaseDao<IDataContext<IRestClient>>, IDao<CustomerDto>
    {
        public CustomerDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(CustomerDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(CustomerDto model)
        {
            throw new NotImplementedException();

        }

        public IEnumerable<CustomerDto> ReadAll(CustomerDto model)
        {
            throw new NotImplementedException();

        }

        public CustomerDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(href, Method.GET, DataFormat.Json);
            var res = restClient.Get<CustomerDto>(restRequest).Data;
            return res;
        }

        public int Update(CustomerDto model)
        {
            throw new NotImplementedException();

        }
    }
}
