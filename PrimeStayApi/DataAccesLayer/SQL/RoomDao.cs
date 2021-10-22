using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class RoomDao : BaseDao<IDataContext>, IDao<RoomEntity>
    {
        public RoomDao(IDataContext dataContext) : base(dataContext)
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
            model.Type = model.Type != null ? "%" + model.Type + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<RoomEntity>($"SELECT * FROM Room WHERE " +
                                                     $"id=ISNULL(@id,id)" +
                                                     $"AND type LIKE ISNULL(@type,type)" +
                                                     $"AND description LIKE ISNULL(@description,description)" +
                                                     $"AND num_of_avaliable LIKE ISNULL(@num_of_avaliable,num_of_avaliable)" +
                                                     $"AND num_of_beds LIKE ISNULL(@num_of_beds,num_of_beds)" +
                                                     $"AND rating LIKE ISNULL(@rating,rating)" +
                                                     $"AND hotel_id LIKE ISNULL(@Hotel_Id,hotel_Id)",
                                                     new { model.Id, model.Type, model.Num_of_avaliable, model.Num_of_beds, model.Description, model.Rating, model.Hotel_Id });

            };

        }

        public RoomEntity ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(RoomEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
