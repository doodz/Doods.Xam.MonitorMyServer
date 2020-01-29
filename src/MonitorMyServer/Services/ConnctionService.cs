using System;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Std;
using Doods.Openmedivault.Http.Std;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace Doods.Xam.MonitorMyServer.Services
{
    public class ConnctionService
    {
        private readonly ILogger _logger;

        public ConnctionService(ILogger logger)
        {
            _logger = logger;
        }


        public bool TestSshConnection(string hostName, int port, string login, string password)
        {
            var connection = new SshConnection(hostName, port, login, password);
            return Ssh(connection, true);
        }

        public bool TestHttpConnection(string hostName, int port, string login, string password)
        {
            var connection = new HttpConnection(hostName, port);
            return Http(connection, login, password, true);
        }


        private bool Http(IConnection connection, string login, string password, bool throwException)
        {
            var testConnectionResult = false;
            try
            {
                var http = new OmvHttpService(_logger, connection);
                testConnectionResult = http.LoginAsync(login, password).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (throwException)
                    throw;
            }

            return testConnectionResult;
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
        private bool Ssh(IConnection connection, bool throwException)
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
    }
}