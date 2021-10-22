using PrimeStay.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;

namespace MVC.DataAccessLayer.DAO
{
    internal class RoomDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomDto>
    {
        public RoomDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(RoomDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomDto> ReadAll(RoomDto model)
        {
            throw new NotImplementedException();
        }

        public RoomDto ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(RoomDto model)
        {
            throw new NotImplementedException();
        }
    }
}
