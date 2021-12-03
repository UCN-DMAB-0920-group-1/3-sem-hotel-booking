using System;
using System.Runtime.Serialization;

namespace API.Controllers
{
    [Serializable]
    internal class DaoExtensionException : Exception
    {
        public DaoExtensionException()
        {
        }

        public DaoExtensionException(string? message) : base(message)
        {
        }

        public DaoExtensionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DaoExtensionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}