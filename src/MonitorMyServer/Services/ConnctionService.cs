﻿using System;
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
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Doods.Openmedivault.Http.Std;
using Doods.Openmedivault.Ssh.Std;
using Doods.Synology.Webapi.Std;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Renci.SshNet;
using Renci.SshNet.Common;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Services
{

    public interface IConnctionService
    {
        Task ChangeHostTask();
        Task Init();
        INasService GetNasService();
        IPackageUpdates GetPackageService();
        Task SelectHost(Host host);
        bool TestSshConnection(string hostName, int port, string login, string password);
        Task<bool> TestHttpConnection(string hostName, int port, string login, string password);
        Task<bool> TestSynoConnection(string hostName, int port, string login, string password);
        Task<bool> TestWebminConnection(string hostName, int port, string login, string password);
    }

    /// <summary>
    ///     Idisposable ???
    /// </summary>
    public class ConnctionService : IConnctionService
    {
        private readonly IDataProvider _dataProvider;
        private readonly IHistoryService _historyService = App.Container.Resolve<IHistoryService>();
        private readonly ILogger _logger = App.Container.Resolve<ILogger>();
        private readonly IMapper _mapper = App.Container.Resolve<IMapper>();
        private readonly IMessageBoxService _messageBoxService;

        //internal static ConnctionService ConnectionService = new ConnctionService();
        private readonly OmvServiceProvider _omvServiceProvider;
        private readonly SshServiceProvider _sshServiceProvider;
        private readonly SynoServiceProvider _synoServiceProvider;
        private readonly WebminServiceProvider _webminServiceProvider;
        private IOmvService _omvService;
        private ISshService _sshService;
        private ISynologyCgiService _synoService;
        private IMessagingCenter _messagingCenter;
        private IPreferences _preferences;
        public ConnctionService(OmvServiceProvider omvServiceProvider, SshServiceProvider sshServiceProvider,
            SynoServiceProvider synoServiceProvider, WebminServiceProvider webminServiceProvider,
            IMessageBoxService messageBoxService, IDataProvider dataProvider, IMessagingCenter messagingCenter, IPreferences preferences)
        {
            _omvServiceProvider = omvServiceProvider;
            _sshServiceProvider = sshServiceProvider;
            _synoServiceProvider = synoServiceProvider;
            _webminServiceProvider = webminServiceProvider;
            _messageBoxService = messageBoxService;
            _dataProvider = dataProvider;
            _messagingCenter = messagingCenter;
            _preferences = preferences;


            _messagingCenter.Subscribe<string,string>(
                this, MessengerKeys.SessionExpired, async (sender, arg) =>
                {
                    
                        await Init();
                });
            _messagingCenter.Subscribe<DataProvider, TableBase>(
                this, MessengerKeys.ItemChanged, async (sender, arg) =>
                {
                    if (arg is Host)
                        await Init();
                });
            
        }


        public ObservableRangeCollection<Host> Hosts { get; } = new ObservableRangeCollection<Host>();
        public Host CurrentHost { get; private set; }
        
        public IPackageUpdates GetPackageService()
        {
            if (CurrentHost == null) return default;

            if (CurrentHost.IsSynoServer) return _synoServiceProvider?.Value;
            if (CurrentHost.IsOmvServer) return _omvServiceProvider?.Value;
            if (CurrentHost.IsSsh) return _sshServiceProvider?.Value;
            if (CurrentHost.IsWebminServer) return _webminServiceProvider?.Value;

            return default;
        }
        public INasService GetNasService()
        {
            if (CurrentHost == null) return default;

            if (CurrentHost.IsSynoServer) return _synoServiceProvider?.Value;
            if (CurrentHost.IsOmvServer) return _omvServiceProvider?.Value;


            return default;
        }

        /// <summary>
        ///     Set host id after login
        /// </summary>
        /// <param name="host"></param>
        private void SetSelectedIdHost(Host host)
        {
            _historyService.UpdateLastLoginAsync(host.Id.GetValueOrDefault());
            _messagingCenter.Send((IConnctionService)this, MessengerKeys.HostChanged, host);
            _preferences.Set(PreferencesKeys.SelectedHostIdKey, host.Id.GetValueOrDefault());
        }

        public async Task Init()
        {
            await GetHosts().ConfigureAwait(false);

            var l = _preferences.Get(PreferencesKeys.SelectedHostIdKey, 0L);

            if (l > 0)
            {
                var findHost = Hosts.FirstOrDefault(h => h.Id != null && h.Id.Value == l);
                if (findHost != null)
                {
                    var loginTask = false;
                    try
                    {
                        await Login(findHost);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        loginTask = true;
                    }
                    finally
                    {
                        if (loginTask) _preferences.Set(PreferencesKeys.SelectedHostIdKey, 0L);
                    }
                }
                else
                {
                    _preferences.Set(PreferencesKeys.SelectedHostIdKey, 0L);
                }
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
            if (!Hosts.Any()) await GetHosts();

            var action = await _messageBoxService.ShowActionSheet(Resource.SelectHost, Resource.Cancel, null,
                Hosts.Select(h => $"{h.Id} - {h.HostName}").ToArray());

            if (action != Resource.Cancel && action != null)
            {
                var split = action.Split('-');
                var id = long.Parse(split[0]);

                var host = Hosts.First(h => h.Id == id);
                await SelectHost(host);
            }
        }

        public async Task SelectHost(Host host)
        {
            if (host.IsSynoServer) _synoService?.LogOut();

            await Login(host);
        }

        public Task LoginFromOnStart(Host host)
        {
            CurrentHost = host;

            GetClient(host);
            if (CurrentHost.IsSsh && !CurrentHost.IsOmvServer)
                return _sshService?.ConnectAsync();
            if (CurrentHost.IsSynoServer && _synoService != null)
                return _synoService.LoginAsync(CurrentHost.UserName, CurrentHost.Password);
            if (_omvService != null)
                return _omvService.Connect(CurrentHost.UserName, CurrentHost.Password);
            return Task.FromResult(0);
        }

        public async Task Login(Host host)
        {
            await LoginFromOnStart(host);
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

            if (host.IsSynoServer)
            {
                var connection = new HttpConnection(host.Url + "/webapi", host.Port);
                var service = new SynologyCgiService(_logger, connection, _mapper);
                _synoService = service;
                _synoServiceProvider.ChangeValue(_synoService);
            }
            else if (host.IsOmvServer || host.Url.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                IRpcClient service;
                if (host.IsSsh)
                {
                    var connection = new SshConnection(host.Url, host.Port, host.UserName, host.Password);
                    service = new OmvSshService(_logger, connection);
                    //SshService 
                    var service2 = new SshService(_logger, _mapper);
                    service2.Init(connection, false);
                    _sshService = service2;
                    _sshServiceProvider.ChangeValue(_sshService);
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

        public async Task<bool> TestHttpConnection(string hostName, int port, string login, string password)
        {
            var connection = new HttpConnection(hostName, port);
            return await Http(connection, login, password, true);
        }

        public Task<bool> TestSynoConnection(string hostName, int port, string login, string password)
        {
            var connection = new HttpConnection(hostName, port);
            return Syno(connection, login, password, true);
        }

        private async Task<bool> Syno(IConnection connection, string login, string password, bool throwException)
        {
            var testConnectionResult = false;
            try
            {
                var syno = new SynologyCgiService(_logger, connection, _mapper);
                testConnectionResult = await syno.LoginAsync(login, password);

                //testConnectionResult = syno.LoginAsync(login, password).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (throwException)
                    throw e;
            }

            return testConnectionResult;
        }


        public Task<bool> TestWebminConnection(string hostName, int port, string login, string password)
        {
            var connection = new HttpConnection(hostName, port);
            return Webmin(connection, login, password, true);
        }

        private async Task<bool> Webmin(IConnection connection, string login, string password, bool throwException)
        {
            var testConnectionResult = false;
            try
            {
                var webmin = new WebminCgiService(_logger, connection, _mapper);
                testConnectionResult = await webmin.LoginAsync(login, password);

                //testConnectionResult = syno.LoginAsync(login, password).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (throwException)
                    throw e;
            }

            return testConnectionResult;
        }

        private async Task<bool> Http(IConnection connection, string login, string password, bool throwException)
        {
            var testConnectionResult = false;
            try
            {
                var http = new OmvHttpService(_logger, connection);
                testConnectionResult = await http.LoginAsync(login, password);
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