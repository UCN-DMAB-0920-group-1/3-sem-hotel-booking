using PrimeStay.MVC.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStay.MVC.DataAccessLayer.DAO
{
    internal class RoomDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomDto>
    {
        public RoomDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(RoomDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomDto> ReadAll(RoomDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest($"/api/room/", Method.GET, DataFormat.Json);
            var res = restClient.Get<IEnumerable<RoomDto>>(restRequest).Data;
            return res;
        }

        public RoomDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(href, Method.GET, DataFormat.Json);
            var res = restClient.Get<RoomDto>(restRequest).Data;
            return res;
        }

        public int Update(RoomDto model)
        {
            throw new NotImplementedException();
        }
    }
}
