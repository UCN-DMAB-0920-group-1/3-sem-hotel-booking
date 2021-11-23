using PrimeStay.WPF.DataAccessLayer.DTO;
using PrimestayWPF.DataAccessLayer;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    internal class HotelDao : BaseDao<IDataContext<IRestClient>>, IDao<HotelDto>
    {
        private readonly string baseEndPoint = "/api/hotel";
        public HotelDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(HotelDto model, string token)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(model);
            restRequest.AddAuthorization(token);
            var response = restClient.Post(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.Created => response.Headers.Where(res => res.Name == "Location").Select(res => res.Value).FirstOrDefault() as string,
                _ => null
            };
        }

        public int Delete(HotelDto model, string token)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.DELETE, DataFormat.Json);
            restRequest.AddHeader("Authorization", "bearer " + token);
            restRequest.AddJsonBody(model);
            var response = restClient.Delete(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.OK => 1,
                _ => -1
            };
        }

        public IEnumerable<HotelDto> ReadAll(HotelDto model, string token)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.GET, DataFormat.Json);
            IRestResponse<IEnumerable<HotelDto>> restResponse = restClient.Get<IEnumerable<HotelDto>>(restRequest);
            return restResponse.Data;
        }

        public HotelDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(href, Method.GET, DataFormat.Json);
            return restClient.Get<HotelDto>(restRequest).Data;
        }

        public int Update(HotelDto model, string token)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.PUT, DataFormat.Json);
            restRequest.AddHeader("Authorization", "bearer " + token);
            restRequest.AddJsonBody(model);
            var response = restClient.Put(restRequest);
            return response.StatusCode switch
            {
                HttpStatusCode.OK => 1,
                _ => -1
            };
        }
    }
}