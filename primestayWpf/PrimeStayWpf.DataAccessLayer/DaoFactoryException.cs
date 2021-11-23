using System;

namespace PrimestayWPF.DataAccessLayer

{
    internal class DaoFactoryException : Exception
    {
        public DaoFactoryException(string message) : base(message)
        {
        }
    }
}
