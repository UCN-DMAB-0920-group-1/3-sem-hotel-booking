using Dapper;
using PrimeStay.DataAccessLayer.DAO;
using PrimeStay.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStay.DataAccessLayer.SQL
{
    internal class HotelDao : BaseDao<IDataContext<IDbConnection>>, IDao<HotelDal>
    {

        public HotelDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(HotelDal model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(HotelDal model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HotelDal> ReadAll(HotelDal model)
        {
            model.Name = model.Name != null ? "%" + model.Name + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;
            model.Staffed_hours = model.Staffed_hours != null ? "%" + model.Staffed_hours + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<HotelDal>($"SELECT * FROM Hotel WHERE " +
                                                                 $"id=ISNULL(@id,id)" +
                                                                 $"AND name LIKE ISNULL(@name,name)" +
                                                                 $"AND description LIKE ISNULL(@description,description)" +
                                                                 $"AND staffed_hours LIKE ISNULL(@staffed_hours,staffed_hours)" +
                                                                 $"AND stars = ISNULL(@stars,stars)",
                                                                 new { model.Id, model.Name, model.Description, model.Staffed_hours, model.Stars });

            };
        }

        public HotelDal ReadById(int id)
        {

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<HotelDal>($@"Select * FROM Hotel WHERE ID = @id", new { id });

            };
        }

        public int Update(HotelDal model)
        {
            throw new System.NotImplementedException();
        }
    }
}