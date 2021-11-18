using Dapper;
using Dapper.Transaction;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class HotelDao : BaseDao<IDataContext<IDbConnection>>, IDao<HotelEntity>
    {
        #region SQL-Queries
        private readonly static string SELECTALLHOTELS = $"SELECT * FROM Hotel WHERE " +
                                                                 $"id=ISNULL(@id,id)" +
                                                                 $"AND name LIKE ISNULL(@name,name)" +
                                                                 $"AND description LIKE ISNULL(@description,description)" +
                                                                 $"AND staffed_hours LIKE ISNULL(@staffed_hours,staffed_hours)" +
                                                                 $"AND stars = ISNULL(@stars,stars)";

        private readonly static string SELECTHOTELBYID = $@"Select * FROM Hotel WHERE ID = @id";

        private readonly static string INSERTHOTEL = "INSERT INTO Hotel (name,description,stars,staffed_hours,location_id) " +
                                                    @"OUTPUT INSERTED.id " +
                                                     "VALUES (@Name,@Description,@Stars,@Staffed_hours,@Location_id)";
        private readonly static string UPDATEHOTEL = "";
        private readonly static string DELETEHOTEL = "";

        #endregion
        public HotelDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(HotelEntity model)
        {
            int res = -1;
            using (IDbTransaction transaction = DataContext.Open().BeginTransaction())
            {
                try
                {
                    res = transaction.ExecuteScalar<int>(INSERTHOTEL, model);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    return res;
                }
            }
            return res;
        }

        public int Delete(HotelEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<int>(DELETEHOTEL, model);

            };
        }

        public IEnumerable<HotelEntity> ReadAll(HotelEntity model)
        {
            model.Name = model.Name != null ? "%" + model.Name + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;
            model.Staffed_hours = model.Staffed_hours != null ? "%" + model.Staffed_hours + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<HotelEntity>(SELECTALLHOTELS, model);

            };
        }

        public HotelEntity ReadById(int id)
        {

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<HotelEntity>(SELECTHOTELBYID, new { id });

            };
        }

        public int Update(HotelEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<int>(UPDATEHOTEL, model);

            };
        }
    }
}