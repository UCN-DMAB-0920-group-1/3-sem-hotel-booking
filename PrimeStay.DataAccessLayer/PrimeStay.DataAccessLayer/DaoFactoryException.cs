using System;

namespace PrimeStay.DataAccessLayer
{
    internal class DaoFactoryException : Exception
    {
        public DaoFactoryException(string message) : base(message)
        {
        }
    }
}
