using PrimeStay.WPF.DataAccessLayer.DTO;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    internal class HotelDao : BaseDao<IDataContext<IRestClient>>, IDao<HotelDto>
    {
        private readonly string baseEndPoint = "/api/hotel";
        public HotelDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(HotelDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(model);
            var response = restClient.Post(restRequest);
            return response.Headers.Where(res => res.Name == "Location").Select(res => res.Value).FirstOrDefault() as string;

        }

        public int Delete(HotelDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.DELETE, DataFormat.Json);
            restRequest.AddJsonBody(model);
            var response = restClient.Post(restRequest);
            return response.Headers.Where(res => res.Name == "Location").Select(res => res.Value).FirstOrDefault() as int? ?? -1; //TODO find header name for response
        }

        public IEnumerable<HotelDto> ReadAll(HotelDto model)
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

        public int Update(HotelDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(baseEndPoint, Method.PUT, DataFormat.Json);
            restRequest.AddJsonBody(model);
            var response = restClient.Post(restRequest);
            return response.Headers.Where(res => res.Name == "Location").Select(res => res.Value).FirstOrDefault() as int? ?? -1; //TODO find header name for response
        }
    }
}