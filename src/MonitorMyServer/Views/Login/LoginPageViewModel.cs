using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Mobile.Std.Config;
using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Mobile.Std.Servicies;
using Doods.Framework.Mobile.Std.Validation;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Validation;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Login
{
    [QueryProperty(nameof(DisplayNameQuery), nameof(DisplayNameQuery))]
    [QueryProperty(nameof(IPAddressQuery), nameof(IPAddressQuery))]
    [QueryProperty(nameof(PortQuery), nameof(PortQuery))]
    [QueryProperty(nameof(UserNameQuery), nameof(UserNameQuery))]
    [QueryProperty(nameof(PasswordQuery), nameof(PasswordQuery))]
    [QueryProperty(nameof(IdQuery), nameof(IdQuery))]
    
    public class LoginPageViewModel : ViewModel
    {
        private ValidatableObjectView<string> _displayName;

        private long _hostId;
        private ValidatableObjectView<string> _hostName;
        private ValidatableObjectView<string> _login;
        private ValidatableObjectView<string> _password;

        private ValidatableObjectView<string> _port;
        private ViewModelStateItem _viewModelStateItem;

        public LoginPageViewModel()
        {
            DisplayName = new ValidatableObjectView<string>(Resource.NameToDisplay, true);
            Port = new ValidatableObjectView<string>(Resource.Port, true, Keyboard.Numeric);
            HostName = new ValidatableObjectView<string>(Resource.HostNameOrIp, true);
            Login = new ValidatableObjectView<string>(Resource.UserNameOrEmail, true);
            Password = new ValidatableObjectView<string>(Resource.Password, true);

            CmdState = new Command(async c => await ValidateConfig());

            ViewModelStateItem = new ViewModelStateItem(this);
            ViewModelStateItem.Title = Resource.ConnectionTest;
            ViewModelStateItem.Description = string.Empty;
            ViewModelStateItem.IsRunning = true;
            ViewModelStateItem.Color = Color.Transparent;
            AddValidations();
        }
        
        public string IdQuery
        {
            set => _hostId = Int64.Parse(value);
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

        public ViewModelStateItem ViewModelStateItem
        {
            get => _viewModelStateItem;
            private set => SetProperty(ref _viewModelStateItem, value);
        }

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
            _hostName.Validations.Add(new IsBadFormetedUrlRule<string>(false)
            {
                ValidationMessage = Resource.PleaseNoHttp
            });

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
                //var test = await Dns.GetHostEntryAsync("192.168.1.47");
                //var test2 = await Dns.GetHostEntryAsync("192.168.1.48");
                //var test3 = await Dns.GetHostAddressesAsync("192.168.1.48");
                //var test4 = await Dns.GetHostAddressesAsync("192.168.1.47");


                //var test5 = Dns.GetHostName();
                //var test6 = await Dns.GetHostAddressesAsync("http://openmediavault4/");
                //var test7 = await Dns.GetHostEntryAsync("192.168.1.12");
                //var test8 = await Dns.GetHostEntryAsync("http://192.168.1.12");
                //var test9 = await Dns.GetHostEntryAsync("https://192.168.1.12");
                //var test10 = await Dns.GetHostEntryAsync("http://openmediavault4");
                //var test11 = await Dns.GetHostEntryAsync("https://openmediavault4");

                if (Validate())
                {
                    ViewModelStateItem.Title = Resource.ConnectionTest;
                    ViewModelStateItem.Description = string.Empty;

                    var sshService = App.Container.Resolve<ISshService>();

                    var connection = new SshConnection(_hostName.Value, int.Parse(_port.Value), _login.Value,
                        _password.Value);
                    if (sshService.TestConnection(connection, true))
                    {
                        await Save();
                        if (App.NavigationServiceType == NavigationServiceType.ViewNavigation)
                        {
                            var mainPage = ((ViewNavigationService) NavigationService).SetRootPage(nameof(MainPage));
                            Application.Current.MainPage = mainPage;
                        }
                        else
                            await NavigationService.GoBack();
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
                IsSsh = true,
                Url = _hostName.Value,
                Port = int.Parse(_port.Value),
                UserName = _login.Value,
                Password = _password.Value
            };

            if (_hostId > 0)
            {
                host.Id = _hostId;
                await DataProvider.UpdateHostAsync(host);
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

        protected override void OnInitializeLoading(LoadingContext context)
        {
            Title = Resource.NewHost;
            base.OnInitializeLoading(context);
        }
    }
}