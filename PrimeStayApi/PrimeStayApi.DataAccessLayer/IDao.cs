using PrimeStayApi.Model;
using System.Collections.Generic;

namespace PrimeStayApi.DataAccessLayer
{
    public interface IDao<T>
    {
        public T ReadById(int id);
        public int Create(T model);
        public int Update(T model);
        public int Delete(T model);
        public IEnumerable<Hotel> ReadAll(int? id, string name, string description, string staffed_hours, int? stars);
    }
}