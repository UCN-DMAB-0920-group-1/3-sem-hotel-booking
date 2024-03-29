﻿using Dapper;
using Dapper.Transaction;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace DataAccessLayer.SQL
{
    internal class BookingDao : BaseDao<IDataContext<IDbConnection>>, IDao<BookingEntity>
    {
        #region SQL-Queries
        private static readonly string SELECT_BOOKING_BY_ID = @"SELECT booking.id, start_date, end_date, guests, room_id, customer_id, room_type_id , room.id as roomIDDISCARD " +
                                                             "FROM booking INNER JOIN room ON booking.room_id = room.id WHERE " +
                                                             "booking.id = ISNULL(@id, booking.id)";

        private static readonly string SELECT_ALL_BOOKINGS = "SELECT booking.id, start_date, end_date, guests, room_id, customer_id, room_type_id , room.id as roomIDDISCARD " +
                                                             "FROM booking INNER JOIN room ON booking.room_id = room.id WHERE " +
                                                             "booking.id = ISNULL(@id, booking.id)" +
                                                             "AND start_date >= ISNULL(@Start_Date, Start_date) " +
                                                             "AND end_date <= ISNULL(@End_Date, End_date) " +
                                                             "AND guests LIKE ISNULL(@guests, guests) " +
                                                             "AND room_id = ISNULL(@room_id, Room_id) " +
                                                             "AND room_type_id = ISNULL(@room_type_id, Room_type_id) " +
                                                             "AND customer_id = ISNULL(@customer_id, Customer_id) ";

        private static readonly string INSERT_BOOKING_RETURN_ID = @"INSERT INTO Booking (Start_date, End_date, Guests,Room_id,Customer_id) " +
                                                                @"OUTPUT INSERTED.id " +
                                                                @"VALUES (@Start_date, @End_date, @Guests,@Room_id,@Customer_id)";

        private static readonly string GET_AVAILABLE_ROOM_RANDOM = "SELECT TOP 1 Room.id FROM Room " +
                                                                "WHERE room.room_type_id = @Room_type_id AND Room.id NOT IN " +
                                                                "( " +
                                                                    "SELECT R.id " +
                                                                    "FROM  Booking B " +
                                                                        "JOIN Room R " +
                                                                        "ON B.room_id = R.id " +
                                                                        "WHERE " +
                                                                        "( " +
                                                                            "(B.start_date <= @start_date AND B.end_date >= @start_date) " +
                                                                            "OR (B.start_date < @end_date AND B.end_date >= @end_date) " +
                                                                            "OR (@start_date <= B.start_date AND @end_date >= B.start_date)" +
                                                                        ") " +
                                                                        "AND Room.room_type_id = @room_type_id " +
                                                                ") " +
                                                                "ORDER BY NEWID()";
        #endregion
        public BookingDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }


        public int Create(BookingEntity model)
        {
            var res = -1;
            try
            {
                using (IDbTransaction transaction = DataContext.Open().BeginTransaction(IsolationLevel.Serializable))
                {
                    model.Room_id = transaction.ExecuteScalar<int>(GET_AVAILABLE_ROOM_RANDOM,
                        model);

                    if (model.Room_id is not null && model.Room_id > 0)
                    {

                        res = transaction.ExecuteScalar<int>(INSERT_BOOKING_RETURN_ID,
                            new { model.Start_date, model.End_date, model.Guests, model.Room_id, model.Customer_id });
                        transaction.Commit();
                    }
                    else transaction.Rollback();
                };
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return res;
        }

        public int Delete(BookingEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BookingEntity> ReadAll(BookingEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<BookingEntity>(SELECT_ALL_BOOKINGS,
                    model);

            };
        }

        public BookingEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<BookingEntity>(SELECT_BOOKING_BY_ID, new { id });
            }
        }

        public int Update(BookingEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
