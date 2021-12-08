using DataAccessLayer.DTO;
using RestSharp;
using System;

namespace DataAccessLayer.DAO
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
                    var dao when dao == typeof(HotelDto) => new HotelDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    var dao when dao == typeof(RoomTypeDto) => new RoomTypeDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    var dao when dao == typeof(LocationDto) => new LocationDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    var dao when dao == typeof(BookingDto) => new BookingDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    var dao when dao == typeof(PriceDto) => new PriceDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    var dao when dao == typeof(CustomerDto) => new CustomerDao(dataContext as IDataContext<IRestClient>) as IDao<T>,

                    _ => null,
                };
            }

            throw new DaoFactoryException("DataContext type not known");
        }
    }
}
