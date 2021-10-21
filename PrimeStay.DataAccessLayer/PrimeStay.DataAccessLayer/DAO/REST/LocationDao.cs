using PrimeStay.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.DAO.REST
{
    internal class LocationDao : BaseDao<IDataContext<IRestClient>>, IDao<Location>
    {
        public LocationDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(Location model)
        {
            throw new NotImplementedException();
        }

        public int Delete(Location model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> ReadAll(Location model)
        {
            throw new NotImplementedException();
        }

        public Location ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Location model)
        {
            throw new NotImplementedException();
        }
    }
}
