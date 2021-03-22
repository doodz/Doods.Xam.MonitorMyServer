using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Openmedivault.Ssh.Std.Requests
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