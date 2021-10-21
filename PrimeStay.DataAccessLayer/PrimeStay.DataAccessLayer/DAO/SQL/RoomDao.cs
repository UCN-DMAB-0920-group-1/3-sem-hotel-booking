using Dapper;
using PrimeStay.DataAccessLayer.DAO;
using PrimeStay.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.SQL
{
    internal class RoomDao : BaseDao<IDataContext<IDbConnection>>, IDao<Room>
    {
        public RoomDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(Room model)
        {
            throw new NotImplementedException();
        }

        public int Delete(Room model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> ReadAll(Room model)
        {
            model.Type = model.Type != null ? "%" + model.Type + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<Room>($"SELECT * FROM Room WHERE " +
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

        public Room ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Room model)
        {
            throw new NotImplementedException();
        }
    }
}
