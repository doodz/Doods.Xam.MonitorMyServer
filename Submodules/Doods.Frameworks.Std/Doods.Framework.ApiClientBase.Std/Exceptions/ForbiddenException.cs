using System;

namespace Doods.Framework.ApiClientBase.Std.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}