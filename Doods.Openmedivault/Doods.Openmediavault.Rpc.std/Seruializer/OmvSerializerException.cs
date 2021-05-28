using System;

namespace Doods.Openmediavault.Rpc.Std.Seruializer
{
    public class OmvSerializerException : Exception
    {
        public OmvSerializerException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}