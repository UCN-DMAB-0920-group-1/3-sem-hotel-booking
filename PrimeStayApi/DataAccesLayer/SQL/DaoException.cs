using System;
using System.Runtime.Serialization;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    [Serializable]
    internal class DaoException : Exception
    {
        public DaoException(string message) : base(message)
        {
        }
    }
}