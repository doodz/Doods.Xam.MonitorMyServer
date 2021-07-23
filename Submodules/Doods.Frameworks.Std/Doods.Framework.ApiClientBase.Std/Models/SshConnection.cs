using Doods.Framework.ApiClientBase.Std.Authentication;

namespace Doods.Framework.ApiClientBase.Std.Models
{
    public class SshConnection : ConnectionBase
    {
        public SshConnection(string host, int port, string username, string password) : base(host, port,
            new Credentials(username, password))
        {
            ConnectionType = ConnectionType.Ssh;
        }


        public SshConnection(string host, string username, string password) : this(host, 22, username, password)
        {
        }
    }
}