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
            model.Street_Address = model.Street_Address != null ? "%" + model.Street_Address + "%" : null;
            model.Zip_code = model.Zip_code != null ? "%" + model.Zip_code + "%" : null;
            model.City = model.City != null ? "%" + model.City + "%" : null;
            model.Country = model.Country != null ? "%" + model.Country + "%" : null;
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<Location>($@"SELECT * FROM Location WHERE " +
                                                               $"id=ISNULL(@id,id) " +
                                                               $"AND Street_Address=ISNULL(@Street_Address,Street_Address)" +
                                                               $"AND Zip_code=ISNULL(@Zip_code,Zip_code)" +
                                                               $"AND City=ISNULL(@City,City)" +
                                                               $"AND Country=ISNULL(@Country,Country)",
                                                               new { model.Id, model.Street_Address, model.Zip_code, model.City, model.Country }
                    );
            }
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
