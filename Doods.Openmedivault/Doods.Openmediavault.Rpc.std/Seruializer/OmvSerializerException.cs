using System;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class OmvSerializerException : Exception
    {
        public OmvSerializerException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}