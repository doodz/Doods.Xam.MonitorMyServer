using System;
using System.Runtime.Serialization;

namespace Doods.Framework.Ssh.Std
{
    [Serializable]
    public class SshExitCodeCommandException : Exception
    {
        private string errorMessage;
        private int statusCode;

        public SshExitCodeCommandException()
        {
        }

        public SshExitCodeCommandException(string message) : base(message)
        {
        }

        public SshExitCodeCommandException(string errorMessage, int statusCode)
        {
            this.errorMessage = errorMessage;
            this.statusCode = statusCode;
        }

        public SshExitCodeCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SshExitCodeCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}