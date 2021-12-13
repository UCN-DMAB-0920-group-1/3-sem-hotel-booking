using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IDao<T>
    {
        T ReadById(int id);
        int Create(T model);
        int Update(T model);
        int Delete(T model);
        IEnumerable<T> ReadAll(T model);
    }
}