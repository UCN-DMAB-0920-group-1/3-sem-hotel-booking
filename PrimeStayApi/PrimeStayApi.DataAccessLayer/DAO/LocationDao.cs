using Dapper;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.DataAccessLayer.DAO
{
    internal class LocationDao : BaseDao<Location>
    {
        public LocationDao(IDataContext dataContext) : base(dataContext)
        {
        }

        public override int Create(Location model)
        {
            throw new NotImplementedException();
        }

        public override int Delete(Location model)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Location> ReadAll(Location model)
        {
            throw new NotImplementedException();
        }

        public override Location ReadById(int id)
        {
            using (IDbConnection connection = DataContext.OpenConnection())
            {
                return connection.QueryFirst<Location>(@$"SELECT * FROM Location WHERE id = @id", new {id});

            };

        }

        public override int Update(Location model)
        {
            throw new NotImplementedException();
        }
    }
}
