using Dapper;
using Dapper.Transaction;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class HotelDao : BaseDao<IDataContext<IDbConnection>>, IDao<HotelEntity>
    {
        #region SQL-Queries
        private readonly static string SELECT_ALL_HOTELS = $"SELECT * FROM Hotel WHERE " +
                                                                 $"id=ISNULL(@id,id)" +
                                                                 $"AND name LIKE ISNULL(@name,name) " +
                                                                 $"AND description LIKE ISNULL(@description,description) " +
                                                                 $"AND staffed_hours LIKE ISNULL(@staffed_hours,staffed_hours) " +
                                                                 $"AND stars = ISNULL(@stars,stars) " +
                                                                 $"AND active = ISNULL(@active,active) ";

        private readonly static string SELECT_HOTEL_BY_ID = $@"Select * FROM Hotel WHERE ID = @id";

        private readonly static string INSERT_HOTEL = "INSERT INTO Hotel (name,description,stars,staffed_hours,location_id, active) " +
                                                    @"OUTPUT INSERTED.id " +
                                                     "VALUES (@Name,@Description,@Stars,@Staffed_hours,@Location_id,@active)";
        private readonly static string UPDATE_HOTEL = "UPDATE Hotel SET name=ISNULL(@name,name), description=ISNULL(@description,description), stars=ISNULL(@stars,stars), staffed_hours=ISNULL(@staffed_hours,staffed_hours), location_id=ISNULL(@location_id,location_id), active=ISNULL(@active,active) WHERE id=@id;";
        //private readonly static string DELETEHOTEL = "DELETE FROM Hotel WHERE id=@id";
        private readonly static string SOFT_DELETE = "UPDATE Hotel SET active=0 WHERE id=@id";

        #endregion
        public HotelDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(HotelEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.ExecuteScalar<int>(INSERT_HOTEL, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public int Delete(HotelEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(SOFT_DELETE, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public IEnumerable<HotelEntity> ReadAll(HotelEntity model)
        {
            model.Name = model.Name != null ? "%" + model.Name + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;
            model.Staffed_hours = model.Staffed_hours != null ? "%" + model.Staffed_hours + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<HotelEntity>(SELECT_ALL_HOTELS, model);
            };
        }

        public HotelEntity ReadById(int id)
        {

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<HotelEntity>(SELECT_HOTEL_BY_ID, new { id });

            };
        }

        public int Update(HotelEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(UPDATE_HOTEL, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
                return res;
            };
        }
    }
}