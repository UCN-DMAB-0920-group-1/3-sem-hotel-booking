using Dapper;
using Dapper.Transaction;
using Models;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace DataAccessLayer.SQL
{
    internal class CustomerDao : BaseDao<IDataContext<IDbConnection>>, IDao<CustomerEntity>
    {
        #region SQL-Queries
        private static readonly string SELECT_CUSTOMER_BY_ID = @"SELECT * FROM Customer WHERE id = @id";

        private static readonly string SELECT_ALL_CUSTOMER = @"SELECT * FROM Customer WHERE " +
                                                            "id = ISNULL(@id,id)" +
                                                            "AND Name = ISNULL(@Name, Name)" +
                                                            "AND Email = ISNULL(@Email, Email)" +
                                                            "AND Birthday = ISNULL(@Birthday, Birthday)" +
                                                            "AND Phone = ISNULL(@Phone, Phone)";

        private static readonly string INSERT_CUSTOMER_RETURN_ID = @"INSERT INTO Customer (Name, Email, Phone,Birthday) " +
                                                                @"OUTPUT INSERTED.id " +
                                                                @"VALUES (@Name, @Email, @Phone,@Birthday)";
        private static readonly string DELETECUSTOMER = "DELETE FROM Customer WHERE id=@id";
        private readonly static string UPDATECUSTOMER = "UPDATE Customer SET Name=@name ,Phone=@Phone, Email=@Email WHERE id=@id";
        #endregion

        public CustomerDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }


        public int Create(CustomerEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    return connection.ExecuteScalar<int>(INSERT_CUSTOMER_RETURN_ID, model);
                }
                catch (System.Exception)
                {

                    return -1;
                }
            }
        }

        public int Delete(CustomerEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(DELETECUSTOMER, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public IEnumerable<CustomerEntity> ReadAll(CustomerEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<CustomerEntity>(SELECT_ALL_CUSTOMER, model);

            };
        }

        public CustomerEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<CustomerEntity>(SELECT_CUSTOMER_BY_ID, new { id });
            }
        }

        public int Update(CustomerEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(UPDATECUSTOMER, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
                return res;
            }
        }
    }
}