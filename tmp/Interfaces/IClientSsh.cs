using Doods.Framework.Std;
using Renci.SshNet;
using System.Threading;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Models;

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
        Task<ISshResponse<T>> ExecuteTaskAsync<T>(ISshRequest request, CancellationToken token);
        Task<ISshResponse<T>> ExecuteTaskAsync<T>(ISshRequest request);
        ShellStream CreateShell();
        ILogger Logger { get; }
        /// <summary>
        /// Try to connect to client
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="throwException"></param>
        /// <returns>true if connection succeeded</returns>
        /// <exception cref="T:Exception"></exception>
        /// <exception cref="T:DoodsApiConnectionExceptionn">SSH session could not be established.</exception>
        /// <exception cref="T:DoodsApiAuthenticationException">Authentication of SSH session failed.</exception>
        bool TestConnection(IConnection connection, bool throwException);
    }
}
