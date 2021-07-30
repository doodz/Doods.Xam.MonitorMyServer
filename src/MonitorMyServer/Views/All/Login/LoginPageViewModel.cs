using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.Mobile.Std.Config;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Mobile.Std.Servicies;
using Doods.Framework.Mobile.Std.Validation;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Doods.Framework.Std.Validation;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Enums;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Resource = Doods.Xam.MonitorMyServer.Resx.Resource;

namespace Doods.Xam.MonitorMyServer.Views.Login
{
    [QueryProperty(nameof(DisplayNameQuery), nameof(DisplayNameQuery))]
    [QueryProperty(nameof(IPAddressQuery), nameof(IPAddressQuery))]
    [QueryProperty(nameof(PortQuery), nameof(PortQuery))]
    [QueryProperty(nameof(UserNameQuery), nameof(UserNameQuery))]
    [QueryProperty(nameof(PasswordQuery), nameof(PasswordQuery))]
    [QueryProperty(nameof(IdQuery), nameof(IdQuery))]
    [QueryProperty(nameof(IsOmvServerQuery), nameof(IsOmvServerQuery))]
    [QueryProperty(nameof(IsRpiQuery), nameof(IsRpiQuery))]
    [QueryProperty(nameof(IsSshQuery), nameof(IsSshQuery))]
    [QueryProperty(nameof(IsSynoQuery), nameof(IsSynoQuery))]
    [QueryProperty(nameof(TypeServiceQuery), nameof(TypeServiceQuery))]
    public class LoginPageViewModel : ViewModelWhithState
    {
        private ValidatableObjectView<string> _displayName;

        private long _hostId;
        private ValidatableObjectView<string> _hostName;

        private bool _isOmvServer;
        private bool _isRpi;
        private bool _isSsh;
        private bool _isSynoServer;
        private bool _isWebminServer;
        private ValidatableObjectView<string> _login;
        private ValidatableObjectView<string> _password;

        private ValidatableObjectView<string> _port;
        private SupportedServicies _typeServicie;

        private readonly IValidationRule<string> HttpUrlRule = new IsBadFormetedUrlRule<string>(true)
        {
            ValidationMessage = Resource.PleaseUseHttp
        };

        private readonly IValidationRule<string> SshUrlRule = new IsBadFormetedUrlRule<string>(false)
        {
            ValidationMessage = Resource.PleaseNoHttp
        };

        public LoginPageViewModel()
        {
            DisplayName =
                new ValidatableObjectView<string>(openmediavault.Hostname, true);
            Port = new ValidatableObjectView<string>(openmediavault.Port, true,
                Keyboard.Numeric);
            HostName = new ValidatableObjectView<string>(openmediavault.Host, true);
            Login = new ValidatableObjectView<string>(openmediavault.Username,
                true);
            Password = new ValidatableObjectView<string>(openmediavault.Password,
                true);

            CmdState = new Command(async c => await ValidateConfig());

            ViewModelStateItem.Title = Resource.ConnectionTest;
            ViewModelStateItem.Description = string.Empty;
            ViewModelStateItem.IsRunning = true;
            ViewModelStateItem.Color = Color.Transparent;
            AddValidations();
        }

        public SupportedServicies TypeService
        {
            get => _typeServicie;
            set => SetProperty(ref _typeServicie, value);
        }

        public bool IsSynoServer
        {
            get => _isSynoServer;
            set => SetProperty(ref _isSynoServer, value);
        }

        public bool IsWebminServer
        {
            get => _isWebminServer;
            set => SetProperty(ref _isWebminServer, value);
        }

        public bool IsOmvServer
        {
            get => _isOmvServer;
            set => SetProperty(ref _isOmvServer, value);
        }

        public bool IsRpi
        {
            get => _isRpi;
            set => SetProperty(ref _isRpi, value);
        }

        public bool IsSsh
        {
            get => _isSsh;
            set => SetProperty(ref _isSsh, value, OnConnectionTypeChanged, null);
        }

        public string TypeServiceQuery
        {
            set => Enum.TryParse(value, out _typeServicie);
        }

        public string IsSshQuery
        {
            set => IsSsh = bool.Parse(value);
        }

        public string IsSynoQuery
        {
            set => IsSynoServer = bool.Parse(value);
        }

        public string IsRpiQuery
        {
            set => IsRpi = bool.Parse(value);
        }

        public string IsOmvServerQuery
        {
            set => IsOmvServer = bool.Parse(value);
        }

        public string IdQuery
        {
            set => _hostId = long.Parse(value);
        }

        public string UserNameQuery
        {
            set => _login.Value = Uri.UnescapeDataString(value);
        }

        public string PasswordQuery
        {
            set => _password.Value = Uri.UnescapeDataString(value);
        }

        public string DisplayNameQuery
        {
            set => _displayName.Value = Uri.UnescapeDataString(value);
        }

        public string IPAddressQuery
        {
            set => _hostName.Value = Uri.UnescapeDataString(value);
        }

        public string PortQuery
        {
            set => _port.Value = Uri.UnescapeDataString(value);
        }

        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public ICommand TestMe => new Command(async () => await ValidateConfig());

        public ICommand ValidateHostNameCommand => new Command(() => ValidateHostName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());


        public ValidatableObjectView<string> DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        public ValidatableObjectView<string> Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public ValidatableObjectView<string> HostName
        {
            get => _hostName;
            set => SetProperty(ref _hostName, value);
        }

        public ValidatableObjectView<string> Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public ValidatableObjectView<string> Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public void SetHost(Host host)
        {
            _displayName.Value = host.HostName;
            _hostName.Value = host.Url;
            _port.Value = host.Port.ToString();
            _login.Value = host.UserName;
            _password.Value = host.Password;
            _hostId = host.Id.GetValueOrDefault();
            _isOmvServer = host.IsOmvServer;
            _isRpi = host.IsRpi;
            _isSsh = host.IsSsh;
            _isSynoServer = host.IsSynoServer;
        }

        public void SetHost(ZeroconfHost zeroconfHost)
        {
            SetHost(zeroconfHost as DataHost);
            //"_ssh._tcp.local.","_https._tcp.local.", "_http._tcp.local."
            if (zeroconfHost.Services.TryGetValue("_ssh._tcp.local.", out var srv)) _port.Value = srv.Port.ToString();
        }

        public void SetHost(DataHost dataHost)
        {
            _displayName.Value = dataHost.DisplayName;
            _hostName.Value = dataHost.IPAddress;
        }

        private void OnConnectionTypeChanged()
        {
            _hostName.Validations.Clear();
            _hostName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.HostNameRequired
            });
            if (IsSsh)
                _hostName.Validations.Add(SshUrlRule);
            else
                _hostName.Validations.Add(HttpUrlRule);

            _hostName.Validate();
        }

        private void AddValidations()
        {
            _displayName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.NameToDisplayRequired
            });

            _hostName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.HostNameRequired
            });
            _hostName.Validations.Add(SshUrlRule);

            _login.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.UsernameRequired
            });

            _password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.PasswordRequired
            });

            _port.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.PortRequired
            });
            _port.Validations.Add(new IsNumericRule<string>(false)
            {
                ValidationMessage = Resource.MustBeInt
            });
        }


        private async Task ValidateConfig()
        {
            try
            {
                if (Validate())
                {
                    ViewModelStateItem.Title = Resource.ConnectionTest;
                    ViewModelStateItem.Description = string.Empty;

                    var connctionService = App.Container.Resolve<IConnctionService>();

                    bool result;
                    if (IsWebminServer)
                    {
                        result = await connctionService.TestWebminConnection(_hostName.Value ,
                            int.Parse(_port.Value),
                            _login.Value,
                            _password.Value);
                    }
                    else if (IsSynoServer)
                        result = await connctionService.TestSynoConnection(_hostName.Value + "/webapi",
                            int.Parse(_port.Value),
                            _login.Value,
                            _password.Value);

                    else if (IsSsh)
                        result = connctionService.TestSshConnection(_hostName.Value, int.Parse(_port.Value),
                            _login.Value,
                            _password.Value);
                    else
                        result = await connctionService.TestHttpConnection(_hostName.Value, int.Parse(_port.Value),
                            _login.Value,
                            _password.Value);


                    if (result)
                    {
                        await Save();
                        if (App.NavigationServiceType == NavigationServiceType.ViewNavigation)
                        {
                            var mainPage = ((ViewNavigationService) NavigationService).SetRootPage(nameof(MainPage));
                            Application.Current.MainPage = mainPage;
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(() => { NavigationService.GoBack(); });
                        }
                    }
                }
            }
            catch (DoodsApiConnectionException ex)
            {
                ViewModelStateItem.Title = Resource.Error;
                ViewModelStateItem.Description = ex.Message;
            }
            catch (DoodsApiAuthenticationException ex)
            {
                ViewModelStateItem.Title = Resource.Error;
                ViewModelStateItem.Description = ex.Message;
            }
            catch (SocketException ex)
            {
                ViewModelStateItem.Title = Resource.Error;
                ViewModelStateItem.Description = ex.Message;
                //if (ex.SocketErrorCode == SocketError.HostNotFound)
                //{
                //    var mes = ex.Message;

                //}
            }
            catch (Exception ex)
            {
                ViewModelStateItem.Title = Resource.Error;
                ViewModelStateItem.Description = ex.Message;
                Logger.Error(ex.Message);
            }
        }

        private async Task Save()
        {
            var count = await DataProvider.CountHostAsync().ConfigureAwait(false);
            var host = new Host
            {
                HostName = _displayName.Value,
                IsSsh = _isSsh,
                IsSynoServer = _isSynoServer,
                Url = _hostName.Value,
                Port = int.Parse(_port.Value),
                UserName = _login.Value,
                Password = _password.Value,
                IsRpi = _isRpi,
                IsOmvServer = _isOmvServer
            };

            if (_hostId > 0)
            {
                host.Id = _hostId;
                await DataProvider.UpdateHostAsync(host).ConfigureAwait(false);
            }
            else
            {
                var id = await DataProvider.InsertHostAsync(host).ConfigureAwait(false);
                Preferences.Set(PreferencesKeys.SelectedHostIdKey, id);
            }
        }


        private bool Validate()
        {
            var isHostNameValid = ValidateHostName();
            var isValidUser = ValidateUserName();
            var isValidPassword = ValidatePassword();
            var isValidDisplayName = ValidateDisplayName();
            var isValidPort = ValidatePort();
            return isHostNameValid && isValidUser && isValidPassword && isValidDisplayName && isValidPort;
        }

        private bool ValidatePort()
        {
            return _port.Validate();
        }

        private bool ValidateHostName()
        {
            return _hostName.Validate();
        }

        private bool ValidateDisplayName()
        {
            return _displayName.Validate();
        }

        private bool ValidateUserName()
        {
            return _login.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private void OnServerTypeChanged(SupportedServicies srv)
        {
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
            switch (TypeService)
            {
                case SupportedServicies.Webmin:
                    IsSsh = false;
                    IsRpi = false;
                    IsOmvServer = false;
                    IsSynoServer = false;
                    IsWebminServer = true;
                    break;
                case SupportedServicies.Synology:
                    IsSsh = false;
                    IsRpi = false;
                    IsOmvServer = false;
                    IsSynoServer = true;
                    IsWebminServer = false;
                    break;
                case SupportedServicies.Openmediavault:
                    IsSsh = false;
                    IsRpi = false;
                    IsOmvServer = true;
                    IsSynoServer = false;
                    IsWebminServer = false;
                    break;
                case SupportedServicies.Openmediavault_SSH:
                    IsSsh = true;
                    IsRpi = false;
                    IsOmvServer = true;
                    IsSynoServer = false;
                    IsWebminServer = false;
                    break;
                case SupportedServicies.Openmediavault_HTTP:
                    IsSsh = false;
                    IsRpi = false;
                    IsOmvServer = true;
                    IsSynoServer = false;
                    IsWebminServer = false;
                    break;
                case SupportedServicies.Openmediavault_HTTPS:
                    IsSsh = false;
                    IsRpi = false;
                    IsOmvServer = true;
                    IsSynoServer = false;
                    IsWebminServer = false;
                    break;
                case SupportedServicies.Unix:
                    IsSsh = true;
                    IsRpi = false;
                    IsOmvServer = false;
                    IsSynoServer = false;
                    IsWebminServer = false;
                    break;
            }

            OnConnectionTypeChanged();
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
            Title = openmediavault.Authentication;
            base.OnInitializeLoading(context);
        }
    }
}