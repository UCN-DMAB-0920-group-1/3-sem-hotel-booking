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
            return typeof(T) switch
            {
                var dao when dao == typeof(Hotel) => new HotelDao(dataContext) as IDao<T>,
                _ => null,

            };

        }
    }
}
