using PrimeStay.WPF.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    internal class RoomTypeDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomTypeDto>
    {
        public RoomTypeDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(RoomTypeDto model, string token)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomTypeDto model, string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomTypeDto> ReadAll(RoomTypeDto model)
        {
            var query_hotelId = $"hotelHref=api/hotel/{model.HotelId}";



            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest($"/api/RoomType?{query_hotelId}", Method.GET, DataFormat.Json);
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

        public int Update(RoomTypeDto model, string token)
        {
            throw new NotImplementedException();
        }
    }
}
