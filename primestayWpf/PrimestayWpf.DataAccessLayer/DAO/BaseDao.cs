namespace PrimeStay.WPF.DataAccessLayer.DAO
{
    internal abstract class BaseDao<T>
    {
        public T DataContext { get; }
        public string AccessToken { get; private set; }

        public BaseDao(T dataContext, string accessToken)
        {
            DataContext = dataContext;
            AccessToken = accessToken;
        }
    }
}