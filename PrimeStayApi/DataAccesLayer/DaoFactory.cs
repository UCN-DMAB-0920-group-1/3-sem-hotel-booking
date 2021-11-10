using PrimeStayApi.DataAccessLayer.SQL;
using PrimeStayApi.Model;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.DAO
{
    public static class DaoFactory
    {
        public static IDao<T> Create<T>(IDataContext dataContext)
        {
            return typeof(T) switch
            {

                var dao when dao == typeof(HotelEntity) => new HotelDao(dataContext as IDataContext<IDbConnection>) as IDao<T>,
                var dao when dao == typeof(LocationEntity) => new LocationDao(dataContext as IDataContext<IDbConnection>) as IDao<T>,
                var dao when dao == typeof(RoomTypeEntity) => new RoomTypeDao(dataContext as IDataContext<IDbConnection>) as IDao<T>,
                var dao when dao == typeof(BookingEntity) => new BookingDao(dataContext as IDataContext<IDbConnection>) as IDao<T>,
                var dao when dao == typeof(PictureEntity) => new PictureDao(dataContext as IDataContext<IDbConnection>) as IDao<T>,


                _ => null,
            };
        }
    }
}
