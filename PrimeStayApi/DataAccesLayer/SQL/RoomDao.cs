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
                return connection.Query<RoomEntity>($"SELECT * FROM Room WHERE " +
                                                     $"id=ISNULL(@id,id)" +
                                                     $"AND room_type_id = ISNULL(@room_type_id,room_type_id)" +
                                                     $"AND room_number = ISNULL(@room_number,room_number)" +
                                                     $"AND notes = ISNULL(@notes,notes)", model);

            };

        }

        public RoomEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<RoomEntity>($"SELECT * FROM Room WHERE id=@id", new { id });

            };
        }

        public int Update(RoomEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
