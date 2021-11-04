using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class BookingDao : BaseDao<IDataContext>, IDao<BookingEntity>
    {
        public BookingDao(IDataContext dataContext) : base(dataContext)
        {
        }
        public int Create(BookingEntity model)
        {
            var res = -1;

            using (IDbConnection connection = DataContext.Open())
            {
                var transaction = connection.BeginTransaction();
                res = connection.ExecuteScalar<int>(@"INSERT INTO Booking (Start_date, End_date, Num_of_guests,Room_id,Customer_id) " +
                                                     @"OUTPUT INSERTED.booking_id " +
                                                     @"VALUES (@Start_date, @End_date, @Num_of_guests,@Room_id,@Customer_id)",
                                                     new { model.Start_date, model.End_date, model.Num_of_guests, model.Room_id, model.Customer_id }, transaction: transaction);

                var avaliableRooms = connection.QueryFirst("SELECT count(*) as Num_of_bookings, (SELECT num_of_avaliable FROM room WHERE id=@room_id)" +
                                                           " as Number_of_avail_Rooms FROM booking WHERE room_id = @room_id AND start_date BETWEEN @start_date AND @end_date" +
                                                                                                       " AND end_date BETWEEN @start_date AND @end_date", new { model.Room_id, model.Start_date, model.End_date }, transaction: transaction);
                System.Console.WriteLine(avaliableRooms.Number_of_avail_Rooms + ":" + avaliableRooms.Num_of_bookings);
                if (avaliableRooms.Number_of_avail_Rooms - avaliableRooms.Num_of_bookings > -1)
                {
                    transaction.Commit();
                    System.Console.WriteLine("commit");
                }
                else
                {
                    res = -1;

                    transaction.Rollback();
                    System.Console.WriteLine("Rollback");
                }


            };
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
                return connection.Query<BookingEntity>(@$"SELECT * FROM Booking WHERE " +
                                                                 $"booking_id=ISNULL(@id,booking_id)" +
                                                                 $"AND start_date >= ISNULL(@Start_date,Start_date)" +
                                                                 $"AND end_date <= ISNULL(@End_date, End_date)" +
                                                                 $"AND num_of_guests LIKE ISNULL(@Num_of_guests, num_of_guests)" +
                                                                 $"AND room_id = ISNULL(@Room_id,Room_id)" +
                                                                 $"AND customer_id = ISNULL(@Customer_id,Customer_id)",
                                                                 new { model.Id, model.Start_date, model.End_date, model.Num_of_guests, model.Room_id, model.Customer_id });

            };
        }

        public BookingEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                //TODO: Alias for booking_id for Dapper auto mappping 
                return connection.QueryFirst<BookingEntity>(@$"SELECT * FROM Booking WHERE booking_id = @id", new { id });
            }
        }

        public int Update(BookingEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
