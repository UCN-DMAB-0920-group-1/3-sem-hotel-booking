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

        public override IEnumerable<Hotel> ReadAll(Dictionary<string, object> map)
        {
            var name = map["name"] != null ? "%" + map["name"] + "%" : null;
            var description = map["description"] != null ? "%" + map["description"] + "%" : null;
            var staffed_hours = map["staffed_hours"] != null ? "%" + map["staffed_hours"] + "%" : null;

            return DataContext.OpenConnection().Query<Hotel>($"SELECT * FROM Hotel WHERE " +
                                                             $"id=ISNULL(@id,id)" +
                                                             $"AND name LIKE ISNULL(@name,name)" +
                                                             $"AND description LIKE ISNULL(@description,description)" +
                                                             $"AND staffed_hours LIKE ISNULL(@staffed_hours,staffed_hours)" +
                                                             $"AND stars = ISNULL(@stars,stars)",
                                                             new { id = map["id"], name, description, staffed_hours, stars = map["stars"] });

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