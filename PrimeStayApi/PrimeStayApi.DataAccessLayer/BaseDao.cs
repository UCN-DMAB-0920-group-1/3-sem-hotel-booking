using PrimeStayApi.Model;
using System.Collections.Generic;

namespace PrimeStayApi.DataAccessLayer
{
    internal abstract class BaseDao<T> : IDao<T>
    {
        public IDataContext DataContext { get; }
        public BaseDao(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public abstract T ReadById(int id);
        public abstract int Create(T model);
        public abstract int Update(T model);
        public abstract int Delete(T model);
        public abstract IEnumerable<Hotel> ReadAll(int? id, string name, string description, string staffed_hours, int? stars);

    }
}