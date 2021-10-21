using Dapper;
using PrimeStay.DataAccessLayer.DAO;
using PrimeStay.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.SQL
{
    internal class LocationDao : BaseDao<IDataContext<IDbConnection>>, IDao<Location>
    {
        public LocationDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
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
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<Location>(@$"SELECT * FROM Location WHERE id = @id", new { id });

            };

        }

        public int Update(Location model)
        {
            throw new NotImplementedException();
        }
    }
}
