using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Openmedivault.Http.Std;
using Doods.Openmedivault.Ssh.Std;
using Doods.Openmedivault.Ssh.Std.Requests;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Renci.SshNet;
using Renci.SshNet.Common;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Services
{
    /// <summary>
    /// Idisposable ???
    /// </summary>
    public class ConnctionService
    {
        private readonly IDataProvider _dataProvider;
        private readonly ILogger _logger = App.Container.Resolve<ILogger>();
        private readonly IMapper _mapper = App.Container.Resolve<IMapper>();
        private readonly IMessageBoxService _messageBoxService;
        //internal static ConnctionService ConnectionService = new ConnctionService();
        private readonly OmvServiceProvider _omvServiceProvider;
        private readonly SshServiceProvider _sshServiceProvider;
        private IOmvService _omvService;
        private ISshService _sshService;

        public ConnctionService(OmvServiceProvider omvServiceProvider, SshServiceProvider sshServiceProvider,
            IMessageBoxService messageBoxService, IDataProvider dataProvider)
        {
            _omvServiceProvider = omvServiceProvider;
            _sshServiceProvider = sshServiceProvider;
            _messageBoxService = messageBoxService;
            _dataProvider = dataProvider;
            MessagingCenter.Subscribe<DataProvider, TableBase>(
                this, MessengerKeys.ItemChanged, async (sender, arg) =>
                {
                    if (arg is Host)
                        await Init();
                });

        }

        public ObservableRangeCollection<Host> Hosts { get; } = new ObservableRangeCollection<Host>();
        public Host CurrentHost { get; private set; }

        private void SetSelectedIdHost(Host host)
        {
            MessagingCenter.Send(this, MessengerKeys.HostChanged, host);
            Preferences.Set(PreferencesKeys.SelectedHostIdKey, host.Id.GetValueOrDefault());
        }

        public async Task Init()
        {
            await GetHosts().ConfigureAwait(false);
           
            var l = Preferences.Get(PreferencesKeys.SelectedHostIdKey, 0L);
           
            if (l > 0)
            {
                var findHost = Hosts.FirstOrDefault(h => h.Id != null && h.Id.Value == l);
                LoginFromOnStart(findHost);
            }
        }

        private async Task GetHosts()
        {
            var hosts = await _dataProvider.GetHostsAsync().ConfigureAwait(false);
            var enumerable = hosts.ToList();
            if (enumerable.Any()) Hosts.ReplaceRange(enumerable);
        }

        public async Task ChangeHostTask()
        {
            if (!Hosts.Any())
            {
                await GetHosts();

            }
            var action = await _messageBoxService.ShowActionSheet(Resource.SelectHost, Resource.Cancel, null,
                Hosts.Select(h => $"{h.Id} - {h.HostName}").ToArray());

            if (action != Resource.Cancel && action != null)
            {
                var split = action.Split('-');
                var id = long.Parse(split[0]);
                Login(Hosts.First(h => h.Id == id));
            }
        }

        public void LoginFromOnStart(Host host)
        {
            CurrentHost = host;

            GetClient(host);
            if (CurrentHost.IsSsh && !CurrentHost.IsOmvServer)
                _sshService?.Connect();
            else
                _omvService?.Connect(CurrentHost.UserName, CurrentHost.Password);
        }

        public void Login(Host host)
        {
            LoginFromOnStart(host);
            SetSelectedIdHost(host);
        }

        public ISshService GetSshClient()
        {
            return _sshService;
        }

        public IOmvService GetOmvClient()
        {
            return _omvService;
        }

        public void GetClient(Host host)
        {
            //ILogger logger, IConnection connection
            if (host.IsOmvServer || host.Url.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                IRpcClient service;
                if (host.IsSsh)
                {
                    var connection = new SshConnection(host.Url, host.Port, host.UserName, host.Password);
                    service = new OmvSshService(_logger, connection);
                    //SshService 
                }
                else
                {
                    var connection = new HttpConnection(host.Url, host.Port);
                    service = new OmvHttpService(_logger, connection);
                }

                var omvservice = new OmvRpcService(service, _logger, _mapper);
                _omvService = omvservice;
                _omvServiceProvider.ChangeValue(_omvService);
            }
            else
            {
                var connection = new SshConnection(host.Url, host.Port, host.UserName, host.Password);
                var service = new SshService(_logger, _mapper);
                service.Init(connection, false);
                _sshService = service;
                _sshServiceProvider.ChangeValue(_sshService);
            }
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