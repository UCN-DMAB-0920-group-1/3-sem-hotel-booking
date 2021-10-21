using PrimeStay.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.DAO.REST
{
    internal class RoomDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomDal>
    {
        public RoomDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(RoomDal model)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomDal model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomDal> ReadAll(RoomDal model)
        {
            throw new NotImplementedException();
        }

        public RoomDal ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(RoomDal model)
        {
            throw new NotImplementedException();
        }
    }
}
