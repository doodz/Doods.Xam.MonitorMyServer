using System;
using System.Runtime.Serialization;

namespace Doods.Openmedivault.Ssh.Std.Exceptions
{
    [Serializable]
    public class OMVException : Exception
    {
        private string errorMessage;
        private int statusCode;

        public OMVException()
        {
        }

        public OMVException(string message) : base(message)
        {
        }

        public OMVException(string errorMessage, int statusCode)
        {
            this.errorMessage = errorMessage;
            this.statusCode = statusCode;
        }

        public OMVException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OMVException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}