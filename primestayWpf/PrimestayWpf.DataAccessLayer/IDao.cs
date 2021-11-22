using System.Collections.Generic;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    public interface IDao<T>
    {
        public T ReadByHref(string href);
        public string Create(T model, string token);
        public int Update(T model, string token);
        public int Delete(T model, string token);
        IEnumerable<T> ReadAll(T model);
    }
}