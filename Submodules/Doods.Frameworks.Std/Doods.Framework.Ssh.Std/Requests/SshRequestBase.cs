using System.Collections.Generic;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Requests
{
    public class SshRequestBase : ISshRequest
    {
        private readonly string _commandText;

        protected ISshSerializer _SshSerializer;
        public IEnumerable<string> NeedGroup = new List<string>();
        public bool NeedSudo;
        protected bool UseSudo;

        public SshRequestBase(string commandText)
        {
            _commandText = commandText;
            _SshSerializer = new SshSerializer();
        }

        public SshRequestBase(string commandText, ISshSerializerSettings settings)
        {
            _commandText = commandText;
            _SshSerializer = new SshSerializer(settings);
        }

        public string CommandText => GetCommandText();

        public IDeserializer Handler => _SshSerializer;


        private string GetCommandText()
        {
            return UseSudo ? $"sudo {_commandText}" : _commandText;
        }
    }
}