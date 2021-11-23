using System;

namespace DataAccessLayer

{
    internal class DaoFactoryException : Exception
    {
        public DaoFactoryException(string message) : base(message)
        {
        }
    }
}
