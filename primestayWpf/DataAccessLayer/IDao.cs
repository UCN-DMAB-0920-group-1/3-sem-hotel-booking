using System.Collections.Generic;

namespace DataAccessLayer

{
    public interface IDao<T>
    {
        T ReadByHref(string href);
        string Create(T model);
        int Update(T model);
        int Delete(T model);
        IEnumerable<T> ReadAll(T model);
    }
}