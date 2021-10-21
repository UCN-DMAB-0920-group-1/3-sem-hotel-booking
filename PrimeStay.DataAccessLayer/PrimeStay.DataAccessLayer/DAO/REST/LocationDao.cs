using PrimeStay.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.DAO.REST
{
    internal class LocationDao : BaseDao<IDataContext<IRestClient>>, IDao<LocationDal>
    {
        public LocationDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(LocationDal model)
        {
            throw new NotImplementedException();
        }

        public int Delete(LocationDal model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationDal> ReadAll(LocationDal model)
        {
            throw new NotImplementedException();
        }

        public LocationDal ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(LocationDal model)
        {
            throw new NotImplementedException();
        }
    }
}
