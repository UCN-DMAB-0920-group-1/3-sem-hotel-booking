using System.Collections.Generic;

namespace PrimeStayApi.DataAccessLayer
{
    public interface IDao<T>
    {
        public IEnumerable<T> ReadAll();
        public T ReadById(int id);
        public int Create(T model);
        public int Update(T model);
        public int Delete(T model);
    }
}