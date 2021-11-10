using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    public class RoomTypeDao : BaseDao<IDataContext<IDbConnection>>, IDao<RoomTypeEntity>
    {
        #region SQL-Queries
        private static readonly string GETNUMBEROFAVAILABLEROOMS = "SELECT room_type_id as id, COUNT(room.id) as Avaliable, type, hotel_id " +
                                                                   "FROM Room " +
                                                                   "INNER JOIN RoomType on Room.room_type_id = RoomType.id " +
                                                                   "WHERE room.id NOT IN( " +
                                                                        "SELECT R.id " +
                                                                        "FROM Booking B " +
                                                                        "JOIN Room R ON B.room_id = R.id " +
                                                                        "WHERE " +
                                                                        "((B.start_date <= @start_date AND B.end_date >= @start_date) " +
                                                                        "OR(B.start_date<@end_date AND B.end_date >= @end_date) " +
                                                                        "OR(@start_date <= B.start_date AND @end_date >= B.start_date)) " +
                                                                        "AND room_type_id IN(select id from RoomType where hotel_id = @hotel_id)) " +
                                                                   "AND room_type_id IN(select id from RoomType where hotel_id = @hotel_id) " +
                                                                   "GROUP BY room_type_id,type,hotel_id ";
        #endregion
        public RoomTypeDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(RoomTypeEntity model)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomTypeEntity model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomTypeEntity> ReadAll(RoomTypeEntity model)
        {
            model.Type = model.Type != null ? "%" + model.Type + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<RoomTypeEntity>($"SELECT * FROM RoomType WHERE " +
                                                     $"id=ISNULL(@id,id)" +
                                                     $"AND type LIKE ISNULL(@type,type)" +
                                                     $"AND description LIKE ISNULL(@description,description)" +
                                                     $"AND beds LIKE ISNULL(@beds,beds)" +
                                                     $"AND rating LIKE ISNULL(@rating,rating)" +
                                                     $"AND hotel_id LIKE ISNULL(@Hotel_Id,hotel_Id)",
                                                     new { model.Id, model.Type, model.beds, model.Description, model.Rating, model.Hotel_Id });

            };

        }

        public RoomTypeEntity ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(RoomTypeEntity model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomTypeEntity> CustomRoomAvailability(int hotel_id, DateTime start_Date, DateTime end_Date)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<RoomTypeEntity>(GETNUMBEROFAVAILABLEROOMS, new { hotel_id, start_Date, end_Date });
            };
        }
    }
}
