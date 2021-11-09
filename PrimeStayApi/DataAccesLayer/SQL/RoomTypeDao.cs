using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class RoomTypeDao : BaseDao<IDataContext<IDbConnection>>, IDao<RoomTypeEntity>
    {
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
    }
}
