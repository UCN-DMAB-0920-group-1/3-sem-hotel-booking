namespace PrimeStay.WPF.DataAccessLayer.DAO
{
    internal abstract class BaseDao<T>
    {
        public T DataContext { get; }
        public string Token { get; set; }
        public BaseDao(T dataContext)
        {
            DataContext = dataContext;
        }
    }
}