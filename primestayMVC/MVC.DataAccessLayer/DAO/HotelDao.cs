using PrimeStay.Model;
using RestSharp;
using System.Collections.Generic;
using System.Data;

namespace MVC.DataAccessLayer.DAO
{
    internal class HotelDao : BaseDao<IDataContext<IRestClient>>, IDao<HotelDto>
    {
        public HotelDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(HotelDto model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(HotelDto model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HotelDto> ReadAll(HotelDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/hotel", Method.GET, DataFormat.Json);
            IRestResponse<IEnumerable<HotelDto>> restResponse = restClient.Get<IEnumerable<HotelDto>>(restRequest);
            return restResponse.Data;
        }

        public HotelDto ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(HotelDto model)
        {
            throw new System.NotImplementedException();
        }
    }
}