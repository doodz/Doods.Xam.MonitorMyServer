using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;
using Renci.SshNet;
using System.Threading;
using System.Threading.Tasks;

namespace Doods.Framework.Ssh.Std
{
    public class ClientSsh : IClientSsh
    {
        public SemaphoreSlim ReadLock { get; private set; } = new SemaphoreSlim(1, 1);
        public SshClient Client { get; private set; }

        public ILogger Logger { get; }
        public ClientSsh(ILogger logger)
        {
            Logger = logger;
        }
        public bool Connect()
        {
            Client = new SshClient("192.168.1.73", "pi", "raspberry");
            Client.Connect();
            return Client.IsConnected;
        }

        public string GetServeurVersion()
        {

            using (var client = new SshClient("192.168.1.73", "pi", "raspberry"))
            {
                client.Connect();

                return client.ConnectionInfo.ServerVersion;

            }
        }

        public bool IsAuthenticated()
        {
            if (Client == null) return false;
            return Client.IsConnected;
        }

        public bool IsConnected()
        {
            return IsAuthenticated();

        }

        public bool CanConnect()
        {
            return true;
        }

        public SshCommand RunQuerry(string cmd)
        {
            return Client.RunCommand(cmd);
        }

        public Task<string> RunCommandAsync(SshCommand cmd, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public ShellStream CreateShell()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ConnectAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
