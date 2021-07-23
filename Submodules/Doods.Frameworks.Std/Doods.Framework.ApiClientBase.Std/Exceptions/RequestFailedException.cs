using System;

namespace Doods.Framework.ApiClientBase.Std.Exceptions
{
    public class RequestFailedException : Exception
    {
        public RequestFailedException(string message, bool isFriendlyMessage) : base(message)
        {
            IsFriendlyMessage = isFriendlyMessage;
        }

        public bool IsFriendlyMessage { get; set; }
    }
}