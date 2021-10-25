using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
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
            throw new System.NotImplementedException();
        }

        public int Delete(BookingEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BookingEntity> ReadAll(BookingEntity model)
        {
  
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<BookingEntity>($"SELECT * FROM Booking WHERE " +
                                                                 $"booking_id=ISNULL(@id,booking_id)" +
                                                                 $"AND start_date >= ISNULL(@Start_date,Start_date)" +
                                                                 $"AND end_date <= ISNULL(@End_date, End_date)" +
                                                                 $"AND num_of_guests LIKE ISNULL(@Num_of_guests, num_of_guests)" +
                                                                 $"AND room_id = ISNULL(@Room_id,Room_id)" + 
                                                                 $"AND customer_id = ISNULL(@Customer_id,Customer_id)",
                                                                 new {model.Id, model.Start_date, model.End_date, model.Num_of_guests, model.Room_id, model.Customer_id });

            };
        }

        public BookingEntity ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(BookingEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
