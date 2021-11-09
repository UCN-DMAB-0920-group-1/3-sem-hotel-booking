using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class LocationDao : BaseDao<IDataContext>, IDao<LocationEntity>
    {
        #region SQL-Queries
        private static readonly string SELECTLOCATIONBYID = @"SELECT * FROM Location WHERE id = @id";

        private static readonly string SELECTALLLOCATIONS = @"SELECT * FROM Location WHERE " +
                                                            "id = ISNULL(@id, id) " +
                                                            "AND Street_Address=ISNULL(@Street_Address, Street_Address)" +
                                                            "AND Zip_code=ISNULL(@Zip_code, Zip_code)" +
                                                            "AND City=ISNULL(@City, City)" +
                                                            "AND Country=ISNULL(@Country, Country)";
        #endregion

        public LocationDao(IDataContext dataContext) : base(dataContext)
        {

        }
        public int Create(LocationEntity model)
        {
            throw new NotImplementedException();
        }

        public int Delete(LocationEntity model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationEntity> ReadAll(LocationEntity model)
        {
            model.Street_Address = model.Street_Address != null ? "%" + model.Street_Address + "%" : null;
            model.Zip_code = model.Zip_code != null ? "%" + model.Zip_code + "%" : null;
            model.City = model.City != null ? "%" + model.City + "%" : null;
            model.Country = model.Country != null ? "%" + model.Country + "%" : null;
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<LocationEntity>(SELECTALLLOCATIONS,
                    new { model.Id, model.Street_Address, model.Zip_code, model.City, model.Country }
                    );
            }
        }

        public LocationEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<LocationEntity>(SELECTLOCATIONBYID, new { id });

            };

        }

        public int Update(LocationEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
