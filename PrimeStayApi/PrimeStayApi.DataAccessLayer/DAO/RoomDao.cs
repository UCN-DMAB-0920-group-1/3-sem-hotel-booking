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

        public override IEnumerable<Room> ReadAll()
        {
            throw new NotImplementedException();
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
