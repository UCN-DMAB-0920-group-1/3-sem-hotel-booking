using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class RoomDao : BaseDao<IDataContext<IDbConnection>>, IDao<RoomEntity>
    {
        #region SQL-Queries 
        private static readonly string SELECTALLROOMS = $"SELECT * FROM Room WHERE " +
                                                     $"id=ISNULL(@id,id)" +
                                                     $"AND room_type_id = ISNULL(@room_type_id,room_type_id)" +
                                                     $"AND room_number = ISNULL(@room_number,room_number)" +
                                                     $"AND notes = ISNULL(@notes,notes)";

        private static readonly string SELECTROOMBYID = $"SELECT * FROM Room WHERE id=@id";


        #endregion
        public RoomDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(RoomEntity model)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomEntity model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomEntity> ReadAll(RoomEntity model)
        {
            model.Notes = model.Notes != null ? "%" + model.Notes + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<RoomEntity>(SELECTALLROOMS, model);

            };

        }

        public RoomEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<RoomEntity>(SELECTROOMBYID, new { id });

            };
        }

        public int Update(RoomEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
