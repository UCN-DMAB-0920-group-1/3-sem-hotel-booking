namespace PrimeStayApi.DataAccessLayer
{
    public interface IDataContext
    {
    }

    public interface IDataContext<T> : IDataContext
    {
        T Open();
    }
}
