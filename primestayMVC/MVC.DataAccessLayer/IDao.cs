using System.Collections.Generic;

namespace PrimeStay.MVC.DataAccessLayer
{
    public interface IDao<T>
    {
        public T ReadByHref(string href);
        public string Create(T model);
        public int Update(T model);
        public int Delete(T model);
        IEnumerable<T> ReadAll(T model);
    }
}