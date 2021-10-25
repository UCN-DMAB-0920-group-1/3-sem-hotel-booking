using PrimeStayApi.Model;


namespace PrimeStayApi.DataAccessLayer.DAO
{
    public static class DaoFactory
    {
        public static IDao<T> Create<T>(IDataContext dataContext)
        {
            return typeof(T) switch
            {
                var dao when dao == typeof(HotelEntity) => new SQL.HotelDao(dataContext as IDataContext) as IDao<T>,
                var dao when dao == typeof(LocationEntity) => new SQL.LocationDao(dataContext as IDataContext) as IDao<T>,
                var dao when dao == typeof(RoomEntity) => new SQL.RoomDao(dataContext as IDataContext) as IDao<T>,
                var dao when dao == typeof(BookingEntity) => new SQL.BookingDao(dataContext as IDataContext) as IDao<T>,
                _ => null,
            };
        }
    }
}
