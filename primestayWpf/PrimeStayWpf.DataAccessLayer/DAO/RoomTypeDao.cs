using PrimeStay.WPF.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{

    internal class RoomTypeDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomTypeDto>
    {
        private readonly string baseEndPoint = "/api/roomType";
        public RoomTypeDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(RoomTypeDto model, string token)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(model);
            var response = restClient.Post(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.Created => response.Headers.Where(res => res.Name == "Location").Select(res => res.Value).FirstOrDefault() as string,
                _ => null
            };
        }

        public int Delete(RoomTypeDto model, string token)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomTypeDto> ReadAll(RoomTypeDto model)
        {
            var query_hotelId = $"hotelHref={model.HotelHref}";



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
