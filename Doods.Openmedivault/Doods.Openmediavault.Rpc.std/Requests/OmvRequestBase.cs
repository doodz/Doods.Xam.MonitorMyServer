using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Openmediavault.Rpc.Std.Seruializer;

namespace Doods.Openmediavault.Rpc.Std.Requests
{
    public abstract class OmvRequestBase : ISshRequest
    {
        public OmvRequestBase(string commandText)
        {
            CommandText = commandText;
            Handler = new OmvSerializer();
        }

        public string CommandText { get; }

        public IDeserializer Handler { get; }
    }
}