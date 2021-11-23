using System;

namespace DataAccessLayer
{
    public interface IDaoDateExtension<T>
    {
        public T CheckAvailability(int id, DateTime start_Date, DateTime end_Date);
    }
}