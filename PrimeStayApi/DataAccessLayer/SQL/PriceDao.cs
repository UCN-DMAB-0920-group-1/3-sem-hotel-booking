using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SQL
{
    internal class PriceDao : BaseDao<IDataContext<IDbConnection>>, IDao<PriceEntity>
    {
        #region SQL-Queries 
        private static readonly string SELECT_ALL_PRICES = $"SELECT * FROM price WHERE " +
                                              $"id=ISNULL(@id,id)" +
                                              $"AND room_type_id = ISNULL(@room_type_id,room_type_id)" +
                                              $"AND value = ISNULL(@value,value)" +
                                              $"AND start_date = ISNULL(@start_date,start_date)";

        private static readonly string SELECT_PRICE_BY_ID = $"SELECT * FROM price WHERE id=@id";


        private readonly static string INSERT_QUERY = "INSERT INTO price (start_date, value, room_type_id) " +
                                                    @"OUTPUT INSERTED.id " +
                                                     "VALUES (@start_date, @value, @room_type_id)";
        #endregion

        public PriceDao(IDataContext<IDbConnection> context) : base(context)
        {

        }

        public int Create(PriceEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.ExecuteScalar<int>(INSERT_QUERY, model);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public int Delete(PriceEntity model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PriceEntity> ReadAll(PriceEntity model)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<PriceEntity>(SELECT_ALL_PRICES, model);
            };
        }


        public PriceEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<PriceEntity>(SELECT_PRICE_BY_ID, new { id });

            };
        }

        public int Update(PriceEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
