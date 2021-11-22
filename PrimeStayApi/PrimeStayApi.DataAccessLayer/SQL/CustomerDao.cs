using Dapper;
using Dapper.Transaction;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class CustomerDao : BaseDao<IDataContext<IDbConnection>>, IDao<CustomerEntity>
    {
        #region SQL-Queries
        private static readonly string SELECTCUSTOMERBYID = @"SELECT * FROM Customer WHERE id = @id";

        private static readonly string SELECTALLCUSTOMER = @"SELECT * FROM Booking WHERE " +
                                                            "id = ISNULL(@id,id)" +
                                                            "AND start_date >= ISNULL(@Start_date, Start_date)" +
                                                            "AND end_date <= ISNULL(@End_date, End_date)" +
                                                            "AND guests LIKE ISNULL(@guests, guests)" +
                                                            "AND room_id = ISNULL(@Room_id, Room_id)" +
                                                            "AND customer_id = ISNULL(@Customer_id, Customer_id)";

        private static readonly string INSERTCUSTOMERRETURNID = @"INSERT INTO Booking (Start_date, End_date, Guests,Room_id,Customer_id) " +
                                                                @"OUTPUT INSERTED.id " +
                                                                @"VALUES (@Start_date, @End_date, @Guests,@Room_id,@Customer_id)";


        #endregion
        public CustomerDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }


        public int Create(CustomerEntity model)
        {
            throw new NotImplementedException();
        }

        public int Delete(CustomerEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CustomerEntity> ReadAll(CustomerEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<CustomerEntity>(SELECTALLCUSTOMER, model);

            };
        }

        public CustomerEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<CustomerEntity>(SELECTCUSTOMERBYID, new { id });
            }
        }

        public int Update(CustomerEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}