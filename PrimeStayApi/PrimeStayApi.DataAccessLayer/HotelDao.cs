using Dapper;
using PrimeStayApi.Model;
using System.Collections.Generic;

namespace PrimeStayApi.DataAccessLayer
{
    internal class HotelDao : BaseDao<Hotel>
    {
        private const string GET_ALL_QUERY = "Select * FROM Hotel";
        public HotelDao(IDataContext dataContext) : base(dataContext)
        {
        }

        public override int Create(Hotel model)
        {
            throw new System.NotImplementedException();
        }

        public override int Delete(Hotel model)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Hotel> ReadAll()
        {
            return DataContext.OpenConnection().Query<Hotel>(GET_ALL_QUERY);
        }

        public override Hotel ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public override int Update(Hotel model)
        {
            throw new System.NotImplementedException();
        }
    }
}