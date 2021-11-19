using System;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    internal class DaoFactoryException : Exception
    {
        public DaoFactoryException(string message) : base(message)
        {
        }
    }
}
