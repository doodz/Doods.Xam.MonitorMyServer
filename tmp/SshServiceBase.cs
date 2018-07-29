using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;
using Renci.SshNet;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doods.Framework.Ssh.Std
{
    public class SshServiceBase : IDisposable, IClientSsh
    {
        private readonly object _lockObj;
        public SemaphoreSlim ReadLock { get; private set; } = new SemaphoreSlim(1, 1);
        protected const int TimeoutInSecond = 60;
        private SshClient _client;

        protected string HostName;
        protected string UserName;
        protected string Password;
        private ShellStream _shell;
        public ILogger Logger { get; }

        public SshClient Client => _client;

        //private ConnectionInfo _connectionInfo;
        protected SshServiceBase(ILogger logger)
        {
            _lockObj = new object();
            Logger = logger;
        }

        protected virtual SshClient GetSshClient()
        {
            return _client ?? (_client = new SshClient(HostName, UserName, Password));
        }


        public Task<string> RunCommandAsync(SshCommand cmd, CancellationToken token)
        {
            if (_client == null) return null;
            try
            {
                using (CancellationTokenSource.CreateLinkedTokenSource(token))
                {
                    //await _readLock.WaitAsync(token);
                    //var request = _client.CreateCommand(cmdStr);


                    var ret = Task.Run(() => _client.RunCommand(cmd.CommandText).Result, token);
                    //var res = Task.Factory.FromAsync(cmd.BeginExecute, cmd.EndExecute, null);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                throw ex;
            }
            finally
            {
                //_readLock.Release();
            }
            //return sshCommand.Result;
        }

        public SshCommand RunQuerry(string cmd)
        {
            return _client.RunCommand(cmd);
        }


        public async Task<bool> ConnectAsync()
        {
            var res = await Task.Run(() => Connect());
            return IsConnected();
            //await Task.Factory.StartNew(Connect);
        }

        public bool Connect()
        {
            lock (_lockObj)
            {
                if (_client == null)
                {
                    GetSshClient();
                }

                try
                {
                    _client.Connect();
                }
                catch
                {
                    // ignored
                }

                return _client.IsConnected;

            }
        }

        public bool IsConnected()
        {
            lock (_lockObj)
            {
                return IsAuthenticated();
            }
        }

        public bool CanConnect()
        {
            return HostName != null && UserName != null && Password != null;
        }

        public bool IsAuthenticated()
        {
            lock (_lockObj)
            {
                if (_client == null) return false;
                return _client.IsConnected;
            }
        }

        public void Dispose()
        {
            lock (_lockObj)
            {
                _client?.Dispose();
                _client = null;
            }
        }

        public ShellStream CreateShell()
        {
            return _shell = Client.CreateShellStream(nameof(SshServiceBase), 0, 0, 0, 0, 1024);
        }
    }
}