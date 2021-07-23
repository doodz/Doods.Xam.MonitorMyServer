using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Requests.YetRequest
{
    public class CustomRequest : SshRequestBase
    {
        public CustomRequest(string commandText) : base(commandText)
        {
        }

        public CustomRequest(string commandText, SshSerializerSettings settings) : base(commandText, settings)
        {
        }
    }
}