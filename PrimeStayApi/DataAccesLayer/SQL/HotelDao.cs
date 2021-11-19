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
        private readonly static string SELECTALLHOTELS = $"SELECT * FROM Hotel WHERE " +
                                                                 $"id=ISNULL(@id,id)" +
                                                                 $"AND name LIKE ISNULL(@name,name) " +
                                                                 $"AND description LIKE ISNULL(@description,description) " +
                                                                 $"AND staffed_hours LIKE ISNULL(@staffed_hours,staffed_hours) " +
                                                                 $"AND stars = ISNULL(@stars,stars) " +
                                                                 $"AND active = ISNULL(@active,active) ";

        private readonly static string SELECTHOTELBYID = $@"Select * FROM Hotel WHERE ID = @id";

        private readonly static string INSERTHOTEL = "INSERT INTO Hotel (name,description,stars,staffed_hours,location_id, active) " +
                                                    @"OUTPUT INSERTED.id " +
                                                     "VALUES (@Name,@Description,@Stars,@Staffed_hours,@Location_id,@active)";
        private readonly static string UPDATEHOTEL = "UPDATE Hotel SET name=@name , description=@description, stars=@stars, staffed_hours=@staffed_hours, location_id=@location_id, active=ISNULL(@active,active) WHERE id=@id;";
        //private readonly static string DELETEHOTEL = "DELETE FROM Hotel WHERE id=@id";
        private readonly static string SOFTDELETE = "UPDATE Hotel SET active=0 WHERE id=@id";

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
                    res = connection.ExecuteScalar<int>(INSERTHOTEL, model);
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
                    res = connection.Execute(SOFTDELETE, model);
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
            model.Active ??= false;
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(UPDATEHOTEL, model);
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