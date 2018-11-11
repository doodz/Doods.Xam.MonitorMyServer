using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Extensions;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;
using Renci.SshNet;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Renci.SshNet.Common;

namespace Doods.Framework.Ssh.Std
{
    public class SshServiceBase : IDisposable, IClientSsh
    {
        protected const int TimeoutInSecond = 60;
        private readonly object _lockObj;
        private SshClient _client;


        private ShellStream _shell;

        protected IConnection Connection;

        //private ConnectionInfo _connectionInfo;
        protected SshServiceBase(ILogger logger)
        {
            _lockObj = new object();
            Logger = logger;
        }

        public SemaphoreSlim ReadLock { get; private set; } = new SemaphoreSlim(1, 1);
        public ILogger Logger { get; }

        public SshClient Client => _client;


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


        public Task<ISshResponse<T>> ExecuteTaskAsync<T>(ISshRequest request)
        {
            return ExecuteTaskAsync<T>(request, CancellationToken.None);
        }

        public Task<ISshResponse<T>> ExecuteTaskAsync<T>(ISshRequest request, CancellationToken token)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var taskCompletionSource = new TaskCompletionSource<ISshResponse<T>>();
            try
            {
                var async = ExecuteAsync<T>(request,
                    (Action<ISshResponse<T>, SshRequestAsyncHandle>) ((response, _) =>
                    {
                        if (token.IsCancellationRequested)
                            taskCompletionSource.TrySetCanceled();
                        else
                            taskCompletionSource.TrySetResult(response);
                    }));
                var registration = token.Register((Action) (() =>
                {
                    async.Abort();
                    taskCompletionSource.TrySetCanceled();
                }));
                taskCompletionSource.Task.ContinueWith((Action<Task<ISshResponse<T>>>) (t => registration.Dispose()),
                    token);
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }

            return taskCompletionSource.Task;
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
                if (_client == null) GetSshClient();

                try
                {
                    _client.Connect();
                }
                catch
                {
                    throw;
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
            return Connection != null;
        }

        public bool IsAuthenticated()
        {
            lock (_lockObj)
            {
                if (_client == null) return false;
                return _client.IsConnected;
            }
        }

        public ShellStream CreateShell()
        {
            return _shell = Client.CreateShellStream(nameof(SshServiceBase), 0, 0, 0, 0, 1024);
        }

        public void Dispose()
        {
            lock (_lockObj)
            {
                _client?.Dispose();
                _client = null;
            }
        }

        protected virtual SshClient GetSshClient()
        {
            var test =
                new PasswordConnectionInfo(Connection.Host, Connection.Port, Connection.Credentials.Login,
                    Connection.Credentials.Password) {Timeout = TimeSpan.FromSeconds(10)};
            return _client ?? (_client = new SshClient(test));
        }

        /// <summary>
        /// Try to connect to client
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="throwException"></param>
        /// <returns>true if connection succeeded</returns>
        /// <exception cref="T:Exception"></exception>
        /// <exception cref="T:DoodsApiConnectionExceptionn">SSH session could not be established.</exception>
        /// <exception cref="T:DoodsApiAuthenticationException">Authentication of SSH session failed.</exception>
        public bool TestConnection(IConnection connection,bool throwException)
        {

            var testConnectionResult = false;
            SshClient client = null;
            try
            {
                var test =
                    new PasswordConnectionInfo(connection.Host, connection.Port, connection.Credentials.Login,
                            connection.Credentials.Password)
                        {Timeout = TimeSpan.FromSeconds(10)};
                client = new SshClient(test);

                //client.HostKeyReceived += (sender, e) =>
                //{
                //    if (true)
                //    {
                //        e.CanTrust = false;
                //    }
                    
                //};

                client.Connect();

                // InvalidOperationException
                // ObjectDisposedException
                // SocketException
                // SshConnectionException
                // SshAuthenticationException
                // ProxyException
                testConnectionResult = client.IsConnected;
            }
            catch (SshConnectionException ex)
            {
                if (throwException)
                    throw new DoodsApiConnectionException(ex.Message);
            }
            catch (SshAuthenticationException ex)
            {
                if (throwException)
                    throw new DoodsApiAuthenticationException(ex.Message);
            }
            catch (Exception e)
            {


                Console.WriteLine(e);
                if (throwException)
                    throw;
            }
            finally
            {
                client?.Dispose();
            }

          
            return testConnectionResult;


        }

        public SshRequestAsyncHandle ExecuteAsync<T>(ISshRequest request,
            Action<ISshResponse<T>, SshRequestAsyncHandle> callback)
        {
            return ExecuteAsync(request,
                (Action<ISshResponse, SshRequestAsyncHandle>) ((response, asyncHandle) =>
                    DeserializeResponse<T>(request, callback, response, asyncHandle)));
        }


        private void DeserializeResponse<T>(ISshRequest request,
            Action<ISshResponse<T>, SshRequestAsyncHandle> callback, ISshResponse response,
            SshRequestAsyncHandle asyncHandle)
        {
            ISshResponse<T> sshResponse1;
            try
            {
                sshResponse1 = Deserialize<T>(request, response);
            }
            catch (Exception ex)
            {
                var restResponse2 = new SshResponse<T>();
                restResponse2.Request = request;
                restResponse2.ResponseStatus = ResponseStatus.Error;
                restResponse2.ErrorMessage = ex.Message;
                restResponse2.ErrorException = ex;
                sshResponse1 = (ISshResponse<T>) restResponse2;
            }

            callback(sshResponse1, asyncHandle);
        }


        private ISshResponse<T> Deserialize<T>(ISshRequest request, ISshResponse raw)
        {
            //request.OnBeforeDeserialization(raw);
            var restResponse = (ISshResponse<T>) new SshResponse<T>();
            try
            {
                restResponse = raw.ToAsyncResponse<T>();
                restResponse.Request = request;
                if (restResponse.ErrorException == null)
                {
                    var handler = request.Handler;
                    if (handler != null) restResponse.Data = handler.Deserialize<T>(raw);
                }
            }
            catch (Exception ex)
            {
                restResponse.ResponseStatus = ResponseStatus.Error;
                restResponse.ErrorMessage = ex.Message;
                restResponse.ErrorException = ex;
            }

            return restResponse;
        }

        public SshRequestAsyncHandle ExecuteAsync(ISshRequest request,
            Action<ISshResponse, SshRequestAsyncHandle> callback)
        {
            return ExecuteAsync(request, callback,
                new Func<SshClient, ISshRequest, Action<SshResponse>, SshCommand>(DoAsGetAsync));
        }


        private SshRequestAsyncHandle ExecuteAsync(ISshRequest request,
            Action<ISshResponse, SshRequestAsyncHandle> callback,
            Func<SshClient, ISshRequest, Action<SshResponse>, SshCommand> getSshRequest)
        {
            //ISsh ssh= this.ConfigureSsh(request);
            var asyncHandle = new SshRequestAsyncHandle();
            var action =
                (Action<SshResponse>) (r => ProcessResponse(request, r, asyncHandle, callback));
            //if (this.UseSynchronizationContext && SynchronizationContext.Current != null)
            //{
            //    SynchronizationContext ctx = SynchronizationContext.Current;
            //    Action<HttpResponse> cb = action;
            //    action = (Action<HttpResponse>)(resp => ctx.Post((SendOrPostCallback)(s => cb(resp)), (object)null));
            //}
            asyncHandle.SshRequest = getSshRequest(Client, request, action);
            return asyncHandle;
        }

        private static void ProcessResponse(ISshRequest request, SshResponse httpResponse,
            SshRequestAsyncHandle asyncHandle, Action<ISshResponse, SshRequestAsyncHandle> callback)
        {
            // SshResponse restResponse = ConvertToRestResponse(request, httpResponse);
            var sshResponse = httpResponse;
            sshResponse.Request = request;

            callback(sshResponse, asyncHandle);
        }

        //SshCommand cmd,
        private static SshCommand DoAsGetAsync(SshClient ssh, ISshRequest request, Action<SshResponse> responseCb)
        {
            var test = ssh.CreateCommand(request.CommandText);

            var asyncResult =
                test.BeginExecute((AsyncCallback) (result => ResponseCallback(result, responseCb)), (object) test);
            return test;
        }

        private static void ResponseCallback(IAsyncResult result, Action<SshResponse> callback)
        {
            var response = new SshResponse()
            {
                ResponseStatus = ResponseStatus.None
            };
            var str = string.Empty;
            try
            {
                var cmd = (SshCommand) result.AsyncState;
                str = cmd.EndExecute(result);
                response.Content = str;
                PopulateErrorForIncompleteResponse(cmd);
                ExtractResponseData(response, cmd);
            }
            catch (Exception e)
            {
                response = ResponseCallbackError(e);
            }

            callback(response);
        }

        private static void ExecuteCallback(SshResponse response, Action<SshResponse> callback)
        {
            callback(response);
        }

        private static SshResponse ResponseCallbackError(Exception e)
        {
            return CreateErrorResponse(e);
        }

        private static SshResponse CreateErrorResponse(Exception ex)
        {
            var sshResponse = new SshResponse();
            //WebException webException;
            //if ((webException = ex as WebException) != null && webException.Status == WebExceptionStatus.RequestCanceled)
            //{
            //    httpResponse.ResponseStatus = this.timeoutState.TimedOut ? ResponseStatus.TimedOut : ResponseStatus.Aborted;
            //    return httpResponse;
            //}
            sshResponse.ErrorMessage = ex.Message;
            sshResponse.ErrorException = ex;
            sshResponse.ResponseStatus = ResponseStatus.Error;
            return sshResponse;
        }

        private static void ExtractResponseData(SshResponse response, SshCommand sshCommand)
        {
            using (sshCommand)
            {
                response.ErrorMessage = sshCommand.Error;
                response.ErrorMessage = sshCommand.CommandText;
                response.ExitStatus = sshCommand.ExitStatus;
                response.Content = sshCommand.Result;
            }
        }

        private static void PopulateErrorForIncompleteResponse(SshCommand response)
        {
            if (response.Error == null) return;

            //response.ErrorException = (Exception)response.ResponseStatus.ToWebException();
            //response.ErrorMessage = response.ErrorException.Message;
        }
    }
}