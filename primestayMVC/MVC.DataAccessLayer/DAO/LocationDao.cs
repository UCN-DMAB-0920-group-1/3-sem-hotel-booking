using MVC.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;

namespace MVC.DataAccessLayer.DAO
{
    internal class LocationDao : BaseDao<IDataContext<IRestClient>>, IDao<LocationDto>
    {
        public LocationDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(LocationDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(LocationDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationDto> ReadAll(LocationDto model)
        {
            throw new NotImplementedException();
        }

        public LocationDto ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(LocationDto model)
        {
            throw new NotImplementedException();
        }
    }
}
