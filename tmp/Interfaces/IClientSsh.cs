using Doods.Framework.Std;
using Renci.SshNet;
using System.Threading;
using System.Threading.Tasks;

namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface IClientSsh
    {

        SemaphoreSlim ReadLock { get; }

        SshClient Client { get; }
        bool Connect();
        Task<bool> ConnectAsync();
        bool IsConnected();
        bool CanConnect();
        bool IsAuthenticated();
        SshCommand RunQuerry(string cmd);
        Task<string> RunCommandAsync(SshCommand cmdStr, CancellationToken token);
        ShellStream CreateShell();
        ILogger Logger { get; }
    }
}
