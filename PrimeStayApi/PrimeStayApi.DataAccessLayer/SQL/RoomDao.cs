using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class RoomDao : BaseDao<IDataContext<IDbConnection>>, IDao<RoomEntity>
    {
        #region SQL-Queries 
        private static readonly string SELECT_ALL_ROOMS = $"SELECT * FROM Room WHERE " +
                                                     $"id=ISNULL(@id,id)" +
                                                     $"AND room_type_id = ISNULL(@room_type_id,room_type_id)" +
                                                     $"AND room_number = ISNULL(@room_number,room_number)" +
                                                     $"AND notes = ISNULL(@notes,notes)";

        private static readonly string SELECT_ROOM_BY_ID = $"SELECT * FROM Room WHERE id=@id";


        private readonly static string INSERT_QUERY = "INSERT INTO Room (room_type_id, room_number, notes, active) " +
                                                    @"OUTPUT INSERTED.id " +
                                                     "VALUES (@room_type_id, @room_number, @notes, @active)";

        private readonly static string UPDATE_QUERY = "UPDATE Room SET room_type_id = @room_type_id, room_number = @room_number, notes = @notes, active = @active WHERE id=@id";
        private readonly static string SOFT_DELETE = "UPDATE Room SET active=0 WHERE id=@id";

        #endregion
        public RoomDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(RoomEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.ExecuteScalar<int>(INSERT_QUERY, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public int Delete(RoomEntity model)
        {
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(SOFT_DELETE, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
            }
            return res;
        }

        public IEnumerable<RoomEntity> ReadAll(RoomEntity model)
        {
            model.Notes = model.Notes != null ? "%" + model.Notes + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<RoomEntity>(SELECT_ALL_ROOMS, model);

            };

        }

        public RoomEntity ReadById(int id)
        {
            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<RoomEntity>(SELECT_ROOM_BY_ID, new { id });

            };
        }

        public int Update(RoomEntity model)
        {
 
            int res = -1;
            using (IDbConnection connection = DataContext.Open())
            {
                try
                {
                    res = connection.Execute(UPDATE_QUERY, model);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e);
                    return res;
                }
                return res;
            };
        }
    }
}
