using System;
using System.Runtime.Serialization;

namespace DataAccessLayer.SQL
{
    [Serializable]
    internal class DaoException : Exception
    {
        public DaoException(string message) : base(message)
        {
        }
    }
}