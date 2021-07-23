using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Std;
using Renci.SshNet;

namespace Doods.Framework.Ssh.Std.Interfaces
{
    public interface IClientSsh
    {
        SemaphoreSlim ReadLock { get; }

        SshClient Client { get; }
        ILogger Logger { get; }
        bool Connect();
        Task<bool> ConnectAsync();
        bool IsConnected();
        bool CanConnect();
        bool IsAuthenticated();
        SshCommand RunQuerry(string cmd);
        Task<string> RunCommandAsync(SshCommand cmdStr, CancellationToken token);
        Task<ISshApiResponse<T>> ExecuteTaskAsync<T>(ISshRequest request, CancellationToken token);
        Task<ISshApiResponse<T>> ExecuteTaskAsync<T>(ISshRequest request);
        ShellClient CreateShell();
        Shell CreateShell(Stream imputStream, Stream outputStream, Stream extendedStream);

        /// <summary>
        ///     Try to connect to client
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="throwException"></param>
        /// <returns>true if connection succeeded</returns>
        /// <exception cref="T:Exception"></exception>
        /// <exception cref="T:DoodsApiConnectionExceptionn">SSH session could not be established.</exception>
        /// <exception cref="T:DoodsApiAuthenticationException">Authentication of SSH session failed.</exception>
        bool TestConnection(IConnection connection, bool throwException);

        ScpClient GetScpClient();
    }
}