namespace PrimeStayApi.DataAccessLayer.DAO
{
    public abstract class BaseDao<T>
    {
        public T DataContext { get; }
        public BaseDao(T dataContext)
        {
            DataContext = dataContext;
        }
    }
}