using PrimeStay.MVC.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PrimeStay.MVC.DataAccessLayer.DAO
{
    internal class RoomDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomTypeDto>
    {
        public RoomDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(RoomTypeDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomTypeDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomTypeDto> ReadAll(RoomTypeDto model)
        {
            var query_hotelId = $"hotelHref=api/hotel/{model.HotelId}";



            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest($"/api/room?{query_hotelId}", Method.GET, DataFormat.Json);
            var res = restClient.Get<IEnumerable<RoomTypeDto>>(restRequest).Data;
            return res; //TODO: Use parameterbinding
        }

        public RoomTypeDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(href, Method.GET, DataFormat.Json);
            var res = restClient.Get<RoomTypeDto>(restRequest).Data;
            return res;
        }

        public int Update(RoomTypeDto model)
        {
            throw new NotImplementedException();
        }
    }
}
