using DataAccessLayer.DTO;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace DataAccessLayer.DAO

{
    internal class RoomDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomDto>
    {
        private readonly string baseEndPoint = "/api/Room/";
        public RoomDao(IDataContext<IRestClient> dataContext, string token) : base(dataContext, token)
        {
        }

        public string Create(RoomDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.POST, DataFormat.Json);
            restRequest.AddAuthorization(AccessToken);
            restRequest.AddJsonBody(model);
            var res = restClient.Post<RoomDto>(restRequest).Data;
            return res.Href;
        }

        public int Delete(RoomDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.DELETE, DataFormat.Json);
            restRequest.AddHeader("Authorization", "bearer " + AccessToken);
            restRequest.AddJsonBody(model);
            restRequest.AddAuthorization(AccessToken);
            var response = restClient.Delete(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.OK => 1,
                HttpStatusCode.NoContent => 1,
                HttpStatusCode.NotFound => 0,
                _ => -1
            };
        }

        public IEnumerable<RoomDto> ReadAll(RoomDto model)
        {
            IRestClient restClient = DataContext.Open();
            var test = baseEndPoint + "?roomTypeHref=" + model.RoomTypeHref;
            IRestRequest restRequest = new RestRequest(test, Method.GET, DataFormat.Json);

            var res = restClient.Get<IEnumerable<RoomDto>>(restRequest).Data;
            return res;
        }

        public RoomDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.GET, DataFormat.Json);

            var res = restClient.Get<RoomDto>(restRequest).Data;
            return res;
        }

        public int Update(RoomDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.PUT, DataFormat.Json);
            restRequest.AddAuthorization(AccessToken);
            restRequest.AddJsonBody(model);
            var response = restClient.Put(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.OK => 1,
                HttpStatusCode.NoContent => 1,
                HttpStatusCode.NotFound => 0,
                _ => -1
            };
        }
    }
}