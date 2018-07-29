using Doods.Framework.Ssh.Std.Interfaces;
using Renci.SshNet;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doods.Framework.Ssh.Std.Base.Queries
{
    internal enum QueryState
    {
        Initialized = 1,
        CommandSent,
        ResultParsed,
        OnError
    }

    public class GenericQuery<T>
    {
        protected readonly IClientSsh Client;
        protected string CmdString;
        protected SshCommand Sshcmd;
        private QueryState _state;
        private string _resultStr;

        public GenericQuery(IClientSsh client)
        {
            Client = client;
            _state = QueryState.Initialized;
        }

        protected IClientSsh GetClient()
        {
            return Client;
        }

        public virtual T Run()
        {
            if (!Client.IsConnected())
            {
                Client.Logger.Info($"Client not connected, Connect.");
                Client.Connect();
            }
            Sshcmd = Client.Client.CreateCommand(CmdString);


            using (Sshcmd)
            {
                Client.Logger.Info($"Running command : {CmdString}.");
                _resultStr = Sshcmd.Execute();


                if (!string.IsNullOrEmpty(Sshcmd.Error))
                {
                    Client.Logger.Info($"Running command ({CmdString}) Error : {Sshcmd.Error}.");
                }

                Client.Logger.Info($"Return Value from command : {_resultStr}.");
                _state = QueryState.CommandSent;
            }

            var result = PaseResult(_resultStr);
            _state = QueryState.ResultParsed;
            //TODO Doods : QueryResult
            var queryResult = ToQueryResult(result);
            return result;
        }


        private QueryResult<T> ToQueryResult(T result)
        {
            var objres = new QueryResult<T>
            {
                Query = CmdString,
                Result = result,
                BashLines = _resultStr,
                ExitStatus = Sshcmd.ExitStatus,
                Error = Sshcmd.Error
            };
            if (!string.IsNullOrEmpty(Sshcmd.Error))
                _state = QueryState.OnError;
            return objres;
        }

        /// <summary>
        /// Create the command, execute it at the client level and parse the result.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<T> RunAsync(CancellationToken token)
        {
            _resultStr = await SendCommandAsync(token);
            _state = QueryState.CommandSent;
            var result = await PaseResultAsync(token);
            _state = QueryState.ResultParsed;
            //TODO Doods : QueryResult
            var queryResult = ToQueryResult(result);
            return result;
        }

        /// <summary>
        /// Create the command end execute it at the client level.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Result from client in string.</returns>
        public async Task<string> SendCommandAsync(CancellationToken token)
        {
            try
            {
                await Client.ReadLock.WaitAsync(token);
                if (token.IsCancellationRequested)
                    return await Task.FromResult<string>(default(string));

                if (!Client.IsConnected())
                {
                    Client.Logger.Info("Client not connected, ConnectAsync.");
                    var res = await Client.ConnectAsync();
                }
                Client.Logger.Info($"Creta command async : {CmdString}.");
                //TODO Doods : SshConnectionException
                Sshcmd = Client.Client.CreateCommand(CmdString);

                Client.Logger.Info($"Running command async : {CmdString}.");
                var restask = Client.RunCommandAsync(Sshcmd, token);
                _resultStr = restask.Result;
                Client.Logger.Info($"Return Value from command async: {_resultStr}.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Client.ReadLock.Release();
            }
            return _resultStr;
        }

        /// <summary>
        /// Parse the result from client 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T> PaseResultAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.FromResult<T>(default(T));
            //using (CancellationTokenSource.CreateLinkedTokenSource(token))
            //{
            var res = await Task.Run(() => PaseResult(_resultStr), token);
            //var res = await Task.Factory.StartNew<T>(() => PaseResult(result), token);
            return res;
            //}
        }

        protected virtual T PaseResult(string result)
        {
            return default(T);
        }
    }
}