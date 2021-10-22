using PrimestayWpf.DataAccessLayer;
using PrimestayWpf.DataAccessLayer.DAO;
using PrimestayWpf.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimestayWpf.DataAccessLayer
{
    public static class DaoFactory
    {
        public static IDao<T> Create<T>(IDataContext dataContext)
        {
            Type DataContextType = dataContext.GetType(); // throws exeption if datacontext is null

            if (typeof(IDataContext<IRestClient>).IsAssignableFrom(DataContextType))
            {
                return typeof(T) switch
                {
                    var dao when dao == typeof(Hotel) => new HotelDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    _ => null,
                };
            }

            throw new NotImplementedException();
        }
    }
}
