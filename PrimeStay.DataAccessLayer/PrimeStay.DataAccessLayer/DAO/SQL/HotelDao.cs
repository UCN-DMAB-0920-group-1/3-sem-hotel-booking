using PrimeStay.Model;
using RestSharp;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.DAO.SQL
{
    internal class HotelDao : BaseDao<IDataContext<IRestClient>>, IDao<Hotel>
    {
        public HotelDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(Hotel model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(Hotel model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Hotel> ReadAll(Hotel model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/hotel", Method.GET, DataFormat.Json);
            IRestResponse<IEnumerable<Hotel>> restResponse = restClient.Get<IEnumerable<Hotel>>(restRequest);
            return restResponse.Data;
        }

        public Hotel ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(Hotel model)
        {
            throw new System.NotImplementedException();
        }
    }
}