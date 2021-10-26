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
            throw new System.NotImplementedException();
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