using PrimeStay.Model;
using RestSharp;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.DAO.REST
{
    internal class HotelDao : BaseDao<IDataContext<IRestClient>>, IDao<HotelDal>
    {
        public HotelDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(HotelDal model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(HotelDal model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HotelDal> ReadAll(HotelDal model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/hotel", Method.GET, DataFormat.Json);
            IRestResponse<IEnumerable<HotelDal>> restResponse = restClient.Get<IEnumerable<HotelDal>>(restRequest);
            return restResponse.Data;
        }

        public HotelDal ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(HotelDal model)
        {
            throw new System.NotImplementedException();
        }
    }
}