using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class RoomTypeDao : BaseDao<IDataContext<IDbConnection>>, IDao<RoomTypeEntity>, IDaoDateExtension<RoomTypeEntity>
    {
        #region SQL-Queries
        private static readonly string SELECTNUMBEROFAVAILABLEROOMS = "SELECT COUNT(*) as Available " +
                                                                "  FROM Room   " +
                                                                "  WHERE room.id NOT IN(   " +
                                                                "  SELECT R.id   " +
                                                                "  FROM Booking B   " +
                                                                "  JOIN Room R ON B.room_id = R.id   " +
                                                                "  WHERE   " +
                                                                "  ((B.start_date <= @start_date AND B.end_date >= @start_date)   " +
                                                                "  OR(B.start_date<@end_date AND B.end_date >= @end_date)   " +
                                                                "  OR(@start_date <= B.start_date AND @end_date >= B.start_date))   " +
                                                                "  AND room_type_id = @room_type_id)   " +
                                                                "AND room_type_id = @room_type_id";
        private static readonly string SELECTONEROOMTYPE = "SELECT * FROM roomType WHERE id=@id";
        private static readonly string SELECTALLROOMTYPE = $"SELECT * FROM RoomType WHERE " +
                                                     $"id=ISNULL(@id,id)" +
                                                     $"AND type LIKE ISNULL(@type,type)" +
                                                     $"AND description LIKE ISNULL(@description,description)" +
                                                     $"AND beds LIKE ISNULL(@beds,beds)" +
                                                     $"AND rating LIKE ISNULL(@rating,rating)" +
                                                     $"AND hotel_id LIKE ISNULL(@Hotel_Id,hotel_Id)";

        private readonly static string INSERT_ROOM_TYPE = "INSERT INTO RoomType (type, beds, description, rating, hotel_id, active) " +
                                            @"OUTPUT INSERTED.id " +
                                             "VALUES (@type,@beds,@description,@rating,@hotel_id,@active)";

        private readonly static string UPDATE_ROOM_TYPE = "UPDATE RoomType SET type=@type, beds=@beds, description=@description, rating=@rating, hotel_id=@hotel_id, active=@active WHERE id=@id;";
        private readonly static string SOFT_DELETE_ROOM_TYPE = "UPDATE RoomType SET active=0 WHERE id=@id";
        #endregion
        public RoomTypeDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(RoomTypeEntity model)
        {

            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.ExecuteScalar<int>(INSERT_ROOM_TYPE, model);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public int Delete(RoomTypeEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(SOFT_DELETE_ROOM_TYPE, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public IEnumerable<RoomTypeEntity> ReadAll(RoomTypeEntity model)
        {
            model.Type = model.Type != null ? "%" + model.Type + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<RoomTypeEntity>(SELECTALLROOMTYPE, model);

            };

        }

        public RoomTypeEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<RoomTypeEntity>(SELECTONEROOMTYPE, new { id });
            };
        }

        public int Update(RoomTypeEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(UPDATE_ROOM_TYPE, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
                return res;
            };
        }

        public RoomTypeEntity CheckAvailability(int room_type_id, DateTime start_Date, DateTime end_Date)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                var res = ReadById(room_type_id);
                res.Avaliable = connection.QueryFirst<int>(SELECTNUMBEROFAVAILABLEROOMS, new { room_type_id, start_Date, end_Date });
                return res;
            };
        }
    }
}
