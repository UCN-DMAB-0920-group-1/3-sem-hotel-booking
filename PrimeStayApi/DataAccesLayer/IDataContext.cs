using System.Data;

namespace PrimeStayApi.DataAccessLayer
{
    public interface IDataContext
    {
        IDbConnection Open();

    }
}
