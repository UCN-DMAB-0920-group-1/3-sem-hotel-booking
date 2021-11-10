using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStayApi.DataAccessLayer
{
    public static class DaoFactory
    {
        public static IDao<T> Create<T>(IDataContext dataContext)
        {
            Type DataContextType = dataContext.GetType(); // throws exeption if datacontext is null

            if (typeof(IDataContext<IDbConnection>).IsAssignableFrom(DataContextType))
            {
                return typeof(T) switch
                {
                    var dao when dao == typeof(Hotel) => new HotelDao(dataContext) as IDao<T>,
                    var dao when dao == typeof(Room) => new RoomDao(dataContext) as IDao<T>,
                    var dao when dao == typeof(Location) => new LocationDao(dataContext) as IDao<T>,
                    _ => null,

                };

            }

            throw new DaoFactoryException("DataContext type not known");
        }
    }
}
