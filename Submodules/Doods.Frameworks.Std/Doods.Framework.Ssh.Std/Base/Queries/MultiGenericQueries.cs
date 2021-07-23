using System;
using System.Threading;
using System.Threading.Tasks;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Base.Queries
{
    public class MultiGenericQueries<T> : GenericQuery<T>
    {
        protected Func<T> Action;


        public MultiGenericQueries(IClientSsh client) : base(client)
        {
        }

        public override T Run()
        {
            if (!Client.IsConnected())
                Client.Connect();

            return Action();
        }

        public override async Task<T> RunAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return await Task.FromResult(default(T));

            try
            {
                await Client.ReadLock.WaitAsync(token);

                if (!Client.IsConnected())
                {
                    var res = await Client.ConnectAsync();
                }

                return await Task.FromResult(Action());
            }
            catch (Exception ex)
            {
                Client.Logger.Error(ex);
                throw ex;
            }
            finally
            {
                Client.ReadLock.Release();
            }
        }
    }
}