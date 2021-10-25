namespace PrimeStay.MVC.DataAccessLayer.DAO
{
    internal abstract class BaseDao<T>
    {
        public T DataContext { get; }
        public BaseDao(T dataContext)
        {
            DataContext = dataContext;
        }
    }
}