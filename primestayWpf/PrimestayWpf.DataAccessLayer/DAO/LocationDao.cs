using PrimestayWPF.DataAccessLayer;
using PrimestayWPF.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PrimestayWPF.DataAccessLayer.DAO

{
    internal class LocationDao : BaseDao<IDataContext<IRestClient>>, IDao<LocationDto>
    {
        public LocationDao(IDataContext<IRestClient> dataContext, string accessToken) : base(dataContext, accessToken)
        {
        }

        public string Create(LocationDto model, string token)
        {
            throw new NotImplementedException();
        }

        public int Delete(LocationDto model, string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationDto> ReadAll(LocationDto model, string token)
        {
            throw new NotImplementedException();
        }

        public int Update(LocationDto model, string token)
        {
            throw new NotImplementedException();
        }
        public LocationDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(href, Method.GET, DataFormat.Json);
            return restClient.Execute<LocationDto>(restRequest).Data;
        }
    }
}
