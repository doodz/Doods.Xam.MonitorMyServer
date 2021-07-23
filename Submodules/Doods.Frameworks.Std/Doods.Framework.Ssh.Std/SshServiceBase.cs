using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Ssh.Std.Extensions;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;
using Renci.SshNet;
using Renci.SshNet.Common;
using Timer = System.Timers.Timer;

namespace Doods.Framework.Ssh.Std
{
    public interface ISubject<T> : IObservable<T>
    {
        void OnNext(T tempData);
    }

    //https://github.com/SuperJMN/UniversalSSH/blob/master/UwpApp/MainViewModel.cs
    public class Subject<T> : ISubject<T>
    {
        private readonly List<IObserver<T>> observers;

        public Subject()
        {
            observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber<T>(observers, observer);
        }

        public void OnNext(T tempData)
        {
            foreach (var observer in observers)
                observer.OnNext(tempData);
        }

        private class Unsubscriber<T> : IDisposable
        {
            private readonly IObserver<T> _observer;
            private readonly List<IObserver<T>> _observers;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null) _observers.Remove(_observer);
            }
        }
    }


    public class StreamPoller : IDisposable
    {
        private readonly Stream output;
        private readonly ISubject<string> textReceivedSubject = new Subject<string>();
        private readonly IDisposable updater;
        private Timer myTimer;

        public StreamPoller(Stream output)
        {
            this.output = output;

            SetTimer();
        }

        public IObservable<string> TextReceived => textReceivedSubject;

        public void Dispose()
        {
            myTimer.Elapsed -= OnTimedEvent;
            //updater?.Dispose();
        }

        private void SetTimer()
        {
            // Create a timer with a 1 second interval.
            myTimer = new Timer(1000);
            // Hook up the Elapsed event for the timer. 
            myTimer.Elapsed += OnTimedEvent;
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Read();
        }

        private void Read()
        {
            var bufflen = output.Length - output.Position;
            var buffer = new byte[bufflen];
            output.Read(buffer, (int) output.Position, (int) bufflen);

            var str = Encoding.UTF8.GetString(buffer);
            if (str != string.Empty) textReceivedSubject.OnNext(str);
        }
    }


    public abstract class ShellReporter : IObserver<string>
    {
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception error)
        {
        }

        public virtual void OnNext(string value)
        {
        }
    }

    public class ShellClient : ShellReporter, IDisposable
    {
        private readonly SshClient _client;
        private readonly Stream extendedOuput = new PipeStream();
        private readonly Stream input = new PipeStream();
        private readonly Stream output = new PipeStream();
        private readonly StreamWriter outputStreamWriter;
        private readonly ISubject<string> textReceivedSubject = new Subject<string>();
        private StreamPoller outputReader;
        private Shell shell;
        private IDisposable textUpdater;

        public ShellClient(SshClient client)
        {
            _client = client;
            outputStreamWriter = new StreamWriter(input) {AutoFlush = true};
        }

        public void Dispose()
        {
            //client?.Dispose();
            shell?.Dispose();
            outputReader?.Dispose();
            textUpdater?.Dispose();
            //outputStreamWriter?.Dispose();
            extendedOuput?.Dispose();
            output?.Dispose();
            input?.Dispose();
        }

        public void Connect()
        {
            if (!_client.IsConnected)
                _client.Connect();
            shell = _client.CreateShell(input, output, extendedOuput);
            shell.Start();
            outputReader = new StreamPoller(output);
            textUpdater = outputReader.TextReceived.Subscribe(this);
        }

        public void SendText(string text)
        {
            outputStreamWriter.WriteLine(text);
        }

        public IDisposable SubscribeTextReceived(IObserver<string> obs)
        {
            return outputReader.TextReceived.Subscribe(obs);
        }

        public void Stop()
        {
            shell?.Stop();
            outputReader?.Dispose();
            textUpdater?.Dispose();
            extendedOuput?.Dispose();
            //outputStreamWriter?.Dispose();
            output?.Dispose();
            input?.Dispose();
        }
    }


    public class SshServiceBase : APIServiceBase, IDisposable, IClientSsh
    {
        protected const int TimeoutInSecond = 60;
        private readonly object _lockObj;


        private ShellClient _shell;


        //private ConnectionInfo _connectionInfo;
        protected SshServiceBase(ILogger logger)
        {
            _lockObj = new object();
            Logger = logger;
        }

        public SemaphoreSlim ReadLock { get; } = new SemaphoreSlim(1, 1);
        public ILogger Logger { get; }

        public SshClient Client { get; private set; }


        public Task<string> RunCommandAsync(SshCommand cmd, CancellationToken token)
        {
            if (Client == null) return null;
            try
            {
                using (CancellationTokenSource.CreateLinkedTokenSource(token))
                {
                    //await _readLock.WaitAsync(token);
                    //var request = _client.CreateCommand(cmdStr);


                    var ret = Task.Run(() => Client.RunCommand(cmd.CommandText).Result, token);


                    //var res = Task.Factory.FromAsync(cmd.BeginExecute, cmd.EndExecute, null);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                throw ex;
            }

            //return sshCommand.Result;
        }


        public Task<ISshApiResponse<T>> ExecuteTaskAsync<T>(ISshRequest request)
        {
            return ExecuteTaskAsync<T>(request, CancellationToken.None);
        }

        public Task<ISshApiResponse<T>> ExecuteTaskAsync<T>(ISshRequest request, CancellationToken token)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var taskCompletionSource = new TaskCompletionSource<ISshApiResponse<T>>();
            try
            {
                var async = ExecuteAsync(request,
                    (Action<ISshApiResponse<T>, SshRequestAsyncHandle>) ((response, _) =>
                    {
                        if (token.IsCancellationRequested)
                            taskCompletionSource.TrySetCanceled();
                        else
                            taskCompletionSource.TrySetResult(response);
                    }));
                var registration = token.Register(() =>
                {
                    async.Abort();
                    taskCompletionSource.TrySetCanceled();
                });
                taskCompletionSource.Task.ContinueWith(t => registration.Dispose(),
                    token);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                taskCompletionSource.TrySetException(ex);
            }

            return taskCompletionSource.Task;
        }

        public SshCommand RunQuerry(string cmd)
        {
            return Client.RunCommand(cmd);
        }


        public Task<bool> ConnectAsync()
        {
            return Task.Run(Connect).ContinueWith(task => IsConnected());
            //return IsConnected();
            //await Task.Factory.StartNew(Connect);
        }

        public bool Connect()
        {
            lock (_lockObj)
            {
                if (Client == null)
                {
                    GetSshClient();
                }
                else
                {
                    if (Client.IsConnected)
                        Client.Disconnect();
                    Client = null;
                    GetSshClient();
                }

                try
                {
                    Client.Connect();
                }
                catch (SshOperationTimeoutException ex)
                {
                    Logger.Debug(ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex.Message);

                    throw;
                }

                return Client.IsConnected;
            }
        }

        public bool IsConnected()
        {
            lock (_lockObj)
            {
                return IsAuthenticated();
            }
        }


        public bool IsAuthenticated()
        {
            lock (_lockObj)
            {
                if (Client == null) return false;
                return Client.IsConnected;
            }
        }


        public Shell CreateShell(Stream imputStream, Stream outputStream, Stream extendedStream)
        {
            return Client.CreateShell(imputStream, outputStream, extendedStream);
        }

        public ShellClient CreateShell()
        {
            //_shell?.Stop();
            return _shell = new ShellClient(Client);
        }

        public ScpClient GetScpClient()
        {
            var test =
                new PasswordConnectionInfo(Connection.Host, Connection.Port, Connection.Credentials.Login,
                        Connection.Credentials.Password)
                    {Timeout = TimeSpan.FromSeconds(10)};
            return new ScpClient(test);
        }

        /// <summary>
        ///     Try to connect to client
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="throwException"></param>
        /// <returns>true if connection succeeded</returns>
        /// <exception cref="T:Exception"></exception>
        /// <exception cref="T:DoodsApiConnectionExceptionn">SSH session could not be established.</exception>
        /// <exception cref="T:DoodsApiAuthenticationException">Authentication of SSH session failed.</exception>
        public bool TestConnection(IConnection connection, bool throwException)
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

        public void Dispose()
        {
            lock (_lockObj)
            {
                Client?.Dispose();
                _shell?.Dispose();
                Client = null;
                _shell = null;
            }
        }

        public Task<IEnumerable<byte[]>> GetFilesAsync(IEnumerable<string> filesPath)
        {
            var lst = new List<byte[]>();
            var scp = GetScpClient();
            if (!scp.IsConnected) scp.Connect();
            scp.RemotePathTransformation = RemotePathTransformation.ShellQuote;

            foreach (var filepath in filesPath)
                using (var ms = new MemoryStream())
                {
                    try
                    {
                        scp.Download(filepath.Trim(), ms);
                        var byteArray = ms.ToArray();
                        lst.Add(byteArray);
                    }
                    catch
                    {
                    }
                }

            scp.Dispose();
            return Task.FromResult(lst.AsEnumerable());
        }

        public Task<byte[]> GetFileAsync(string filePath)
        {
            var scp = GetScpClient();
            if (!scp.IsConnected) scp.Connect();
            scp.RemotePathTransformation = RemotePathTransformation.ShellQuote;
            using (var ms = new MemoryStream())
            {
                try
                {
                    scp.Download(filePath.Trim(), ms);
                    var byteArray = ms.ToArray();
                    return Task.FromResult(byteArray);
                }
                catch
                {
                }
                finally
                {
                    scp.Dispose();
                }
            }

            return Task.FromResult(default(byte[]));
        }

        protected virtual SshClient GetSshClient()
        {
            var test =
                new PasswordConnectionInfo(Connection.Host, Connection.Port, Connection.Credentials.Login,
                    Connection.Credentials.Password) {Timeout = TimeSpan.FromSeconds(10)};
            return Client ?? (Client = new SshClient(test));
        }

        public SshRequestAsyncHandle ExecuteAsync<T>(ISshRequest request,
            Action<ISshApiResponse<T>, SshRequestAsyncHandle> callback)
        {
            return ExecuteAsync(request,
                (response, asyncHandle) =>
                    DeserializeResponse(request, callback, response, asyncHandle));
        }


        private void DeserializeResponse<T>(ISshRequest request,
            Action<ISshApiResponse<T>, SshRequestAsyncHandle> callback, ISshApiResponse apiResponse,
            SshRequestAsyncHandle asyncHandle)
        {
            ISshApiResponse<T> sshApiResponse1;
            try
            {
                sshApiResponse1 = Deserialize<T>(request, apiResponse);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                var restResponse2 = new SshApiResponse<T>();
                restResponse2.Request = request;
                restResponse2.ResponseStatus = ResponseStatus.Error;
                restResponse2.ErrorMessage = ex.Message;
                restResponse2.ErrorException = ex;
                sshApiResponse1 = restResponse2;
            }

            callback(sshApiResponse1, asyncHandle);
        }


        private ISshApiResponse<T> Deserialize<T>(ISshRequest request, ISshApiResponse raw)
        {
            //request.OnBeforeDeserialization(raw);
            var restResponse = (ISshApiResponse<T>) new SshApiResponse<T>();
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
                Logger.Error(ex);
                restResponse.ResponseStatus = ResponseStatus.Error;
                restResponse.ErrorMessage = ex.Message;
                restResponse.ErrorException = ex;
            }

            return restResponse;
        }

        public SshRequestAsyncHandle ExecuteAsync(ISshRequest request,
            Action<ISshApiResponse, SshRequestAsyncHandle> callback)
        {
            return ExecuteAsync(request, callback,
                DoAsGetAsync);
        }


        private SshRequestAsyncHandle ExecuteAsync(ISshRequest request,
            Action<ISshApiResponse, SshRequestAsyncHandle> callback,
            Func<SshClient, ISshRequest, Action<SshApiResponse>, SshCommand> getSshRequest)
        {
            //ISsh ssh= this.ConfigureSsh(request);
            var asyncHandle = new SshRequestAsyncHandle();
            var action =
                (Action<SshApiResponse>) (r => ProcessResponse(request, r, asyncHandle, callback));
            //if (this.UseSynchronizationContext && SynchronizationContext.Current != null)
            //{
            //    SynchronizationContext ctx = SynchronizationContext.Current;
            //    Action<HttpResponse> cb = action;
            //    action = (Action<HttpResponse>)(resp => ctx.Post((SendOrPostCallback)(s => cb(resp)), (object)null));
            //}
            asyncHandle.SshRequest = getSshRequest(Client, request, action);
            return asyncHandle;
        }

        private void ProcessResponse(ISshRequest request, SshApiResponse sshApiResponse,
            SshRequestAsyncHandle asyncHandle, Action<ISshApiResponse, SshRequestAsyncHandle> callback)
        {
            // SshApiResponse restResponse = ConvertToRestResponse(request, httpResponse);
            //var sshApiResponse = httpResponse;
            sshApiResponse.Request = request;

            callback(sshApiResponse, asyncHandle);
        }


        //SshCommand cmd,
        private SshCommand DoAsGetAsync(SshClient ssh, ISshRequest request, Action<SshApiResponse> responseCb)
        {
            var cmd = ssh.CreateCommand(request.CommandText);

            var asyncResult =
                cmd.BeginExecute(result => { ResponseCallback(result, responseCb, cmd); },
                    cmd);
            var b = asyncResult.IsCompleted;
            return cmd;
        }

        private void ResponseCallback(IAsyncResult result, Action<SshApiResponse> callback, SshCommand sshCommand)
        {
            var response = new SshApiResponse
            {
                ResponseStatus = ResponseStatus.None
            };
            //var str = string.Empty;
            try
            {
                //WaitHandle.WaitAny(new[] {result.AsyncWaitHandle});


                //var cmd = (SshCommand) result.AsyncState;

                //str = cmd.EndExecute(result);

                //var b = result.IsCompleted;

                //apiResponse.Content = str;
                using (sshCommand)
                {
                    ExtractResponseData(response, sshCommand);
                    PopulateErrorForIncompleteResponse(response, sshCommand);
                }
            }
            catch (Exception e)
            {
                response = ResponseCallbackError(e);
            }

            callback(response);
        }

        private void ExecuteCallback(SshApiResponse apiResponse, Action<SshApiResponse> callback)
        {
            callback(apiResponse);
        }

        private SshApiResponse ResponseCallbackError(Exception e)
        {
            return CreateErrorResponse(e);
        }

        private SshApiResponse CreateErrorResponse(Exception ex)
        {
            var sshResponse = new SshApiResponse();
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

        private void ExtractResponseData(SshApiResponse apiResponse, SshCommand sshCommand)
        {
            apiResponse.Content = sshCommand.Result;
            apiResponse.ResponseStatus = ResponseStatus.Completed;
            apiResponse.StatusCode = sshCommand.ExitStatus;
        }

        private void PopulateErrorForIncompleteResponse(SshApiResponse apiResponse, SshCommand sshCommand)
        {
            if (sshCommand.ExitStatus > 0)
            {
                apiResponse.ResponseStatus = ResponseStatus.Error;
                apiResponse.ErrorMessage = sshCommand.Error;
                apiResponse.ExitStatus = sshCommand.ExitStatus;
            }

            //apiResponse.ErrorException = (Exception)apiResponse.ResponseStatus.ToWebException();
            //apiResponse.ErrorMessage = apiResponse.ErrorException.Message;
        }
    }
}