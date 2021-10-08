using Dapper;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.DataAccessLayer
{
    internal class RoomDao : BaseDao<Room>
    {
        public RoomDao(IDataContext dataContext) : base(dataContext)
        {
        }

        public override int Create(Room model)
        {
            throw new NotImplementedException();
        }

        public override int Delete(Room model)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Room> ReadAll(Room model)
        {
            model.Type = model.Type != null ? "%" + model.Type + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;

            return DataContext.OpenConnection().Query<Room>($"SELECT * FROM room WHERE " +
                                                 $"id=ISNULL(@id,id)" +
                                                 $"AND type LIKE ISNULL(@type,type)" +
                                                 $"AND description LIKE ISNULL(@description,description)" +
                                                 $"AND num_of_avaliable LIKE ISNULL(@num_of_avaliable,num_of_avaliable)" +
                                                 $"AND num_of_beds LIKE ISNULL(@num_of_beds,num_of_beds)" +
                                                 $"AND description LIKE ISNULL(@description,description)" +
                                                 $"AND rating LIKE ISNULL(@rating,rating)" +
                                                 $"AND hotelId LIKE ISNULL(@hotelId,hotelId)" +
                                                 new { model.Id, model.Type, model.Num_of_avaliable, model.Num_of_beds, model.Description, model.Rating, model.HotelId });

        }

        public override Room ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public override int Update(Room model)
        {
            throw new NotImplementedException();
        }
    }
}
