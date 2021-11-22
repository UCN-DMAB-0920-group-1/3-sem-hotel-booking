using System;

namespace PrimeStayApi.DataAccessLayer
{
    internal class DaoFactoryException : Exception
    {
        public DaoFactoryException(string message) : base(message)
        {
        }
    }
}
