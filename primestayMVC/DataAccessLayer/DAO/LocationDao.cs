using DataAccessLayer;
using DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.DAO
{
    internal class LocationDao : BaseDao<IDataContext<IRestClient>>, IDao<LocationDto>
    {
        public LocationDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(LocationDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(LocationDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationDto> ReadAll(LocationDto model)
        {
            throw new NotImplementedException();
        }

        public LocationDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(href, Method.GET, DataFormat.Json);
            return restClient.Execute<LocationDto>(restRequest).Data;
        }

        public int Update(LocationDto model)
        {
            throw new NotImplementedException();
        }
    }
}
