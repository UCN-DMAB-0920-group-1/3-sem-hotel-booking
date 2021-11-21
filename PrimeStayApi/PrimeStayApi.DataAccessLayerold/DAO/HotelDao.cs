using Dapper;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer
{
    internal class HotelDao : BaseDao<Hotel>
    {

        public HotelDao(IDataContext dataContext) : base(dataContext)
        {
        }

        public override int Create(Hotel model)
        {
            throw new System.NotImplementedException();
        }

        public override int Delete(Hotel model)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Hotel> ReadAll(Hotel model)
        {
            model.Name = model.Name != null ? "%" + model.Name + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;
            model.Staffed_hours = model.Staffed_hours != null ? "%" + model.Staffed_hours + "%" : null;

            using (IDbConnection connection = DataContext.OpenConnection())
            {
                return connection.Query<Hotel>($"SELECT * FROM Hotel WHERE " +
                                                                 $"id=ISNULL(@id,id)" +
                                                                 $"AND name LIKE ISNULL(@name,name)" +
                                                                 $"AND description LIKE ISNULL(@description,description)" +
                                                                 $"AND staffed_hours LIKE ISNULL(@staffed_hours,staffed_hours)" +
                                                                 $"AND stars = ISNULL(@stars,stars)",
                                                                 new { model.Id, model.Name, model.Description, model.Staffed_hours, model.Stars });

            };
        }

        public override Hotel ReadById(int id)
        {

            using (IDbConnection connection = DataContext.OpenConnection())
            {
                return connection.QueryFirst<Hotel>($@"Select * FROM Hotel WHERE ID = @id", new { id });

            };
        }

        public override int Update(Hotel model)
        {
            throw new System.NotImplementedException();
        }
    }
}