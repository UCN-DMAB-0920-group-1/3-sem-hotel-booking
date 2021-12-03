using System;
using System.Runtime.Serialization;

namespace DataAccessLayer.DTO
{
    [Serializable]
    internal class HrefFormatException : Exception
    {
        public HrefFormatException()
        {
        }

        public HrefFormatException(string? message) : base(message)
        {
        }

        public HrefFormatException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HrefFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}