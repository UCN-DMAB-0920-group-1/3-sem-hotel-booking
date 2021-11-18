namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    public interface IDataContext
    {

    }

    public interface IDataContext<T> : IDataContext
    {
        T Open();
    }
}
