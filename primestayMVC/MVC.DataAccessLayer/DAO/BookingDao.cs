using PrimeStay.MVC.DataAccessLayer.DTO;
using RestSharp;
using System.Collections.Generic;

namespace PrimeStay.MVC.DataAccessLayer.DAO
{
    internal class BookingDao : BaseDao<IDataContext<IRestClient>>, IDao<BookingDto>
    {
        public BookingDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(BookingDto model)
        {
            IRestClient client = DataContext.Open();
            IRestRequest request = new RestRequest("/api/booking", Method.POST, DataFormat.Json).AddJsonBody(model);
            var res = client.Execute<int>(request).Data;
            return res;
        }

        public int Delete(BookingDto model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BookingDto> ReadAll(BookingDto model)
        {
            throw new System.NotImplementedException();
        }

        public BookingDto ReadByHref(string href)
        {
            throw new System.NotImplementedException();
        }

        public int Update(BookingDto model)
        {
            throw new System.NotImplementedException();
        }
    }
}