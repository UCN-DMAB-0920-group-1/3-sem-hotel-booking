using Dapper;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public override IEnumerable<Hotel> ReadAll(int? id, string name, string description, string staffed_hours, int? stars)
        {
            name = "%" + name + "%" ?? name;
            description = "%" + description + "%" ?? description;
            staffed_hours = "%" + staffed_hours + "%" ?? staffed_hours;
            return DataContext.OpenConnection().Query<Hotel>($"SELECT * FROM Hotel WHERE " +
                                                             $"id=ISNULL(@id,id) AND" +
                                                             $" name LIKE ISNULL(@name,name)",
                                                             new { id, name, description, staffed_hours, stars });

        }

        public override Hotel ReadById(int id)
        {
            string GET_BY_ID_QUERY = $"Select * FROM Hotel WHERE ID = {id}";
            return DataContext.OpenConnection().QueryFirst<Hotel>(GET_BY_ID_QUERY);
        }

        public override int Update(Hotel model)
        {
            throw new System.NotImplementedException();
        }
    }
}