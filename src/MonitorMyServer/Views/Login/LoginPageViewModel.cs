using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std.Validation;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Login
{

    public class LoginPageViewModel : ViewModel
    {
        private ValidatableObject<string> _hostName;
        private ValidatableObject<string> _longin;
        private ValidatableObject<string> _password;
        private ViewModelStateItem _viewModelStateItem;


        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand ValidateHostNameCommand => new Command(() => ValidateHostName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());


        public LoginPageViewModel()
        {
          

            HostName = new ValidatableObject<string>(true);
            Login = new ValidatableObject<string>(true);
            Password = new ValidatableObject<string>(true);
        }



        private void AddValidations()
        {

            _hostName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.HostNameRequired
            });

            _longin.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage =Resource.UsernameRequired
            });

            _password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.PasswordRequired
            });
        }


        private bool Validate()
        {
            bool isHostNameValid = ValidateHostName();
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();
            return isHostNameValid && isValidUser && isValidPassword;
        }

        private bool ValidateHostName()
        {
            return _hostName.Validate();
        }
        private bool ValidateUserName()
        {
            return _longin.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        public ViewModelStateItem ViewModelStateItem
        {
            get => _viewModelStateItem;
            private set => SetProperty(ref _viewModelStateItem, value);
        }

        public ValidatableObject<string> HostName
        {
            get => _hostName;
            set => SetProperty(ref _hostName, value);
        }

        public ValidatableObject<string> Login
        {
            get => _longin;
            set => SetProperty(ref _longin, value);
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
            Title = Resource.NewHost;
            base.OnInitializeLoading(context);
        }
    }
}