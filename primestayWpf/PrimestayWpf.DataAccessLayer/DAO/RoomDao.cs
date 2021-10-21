using PrimestayWpf.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimestayWpf.DataAccessLayer.DAO
{
    internal class RoomDao : BaseDao<IDataContext<IRestClient>>, IDao<Room>
    {
        public RoomDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(Room model)
        {
            throw new NotImplementedException();
        }

        public int Delete(Room model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> ReadAll(Room model)
        {
            throw new NotImplementedException();
        }

        public Room ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Room model)
        {
            throw new NotImplementedException();
        }
    }
}
