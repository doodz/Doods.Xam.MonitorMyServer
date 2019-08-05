using System;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.Validation;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Validation;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.AddCustomCommand
{
    [QueryProperty(nameof(NameQuery), nameof(NameQuery))]
    [QueryProperty(nameof(CommandStringQuery), nameof(CommandStringQuery))]

    [QueryProperty(nameof(IdQuery), nameof(IdQuery))]
    public class AddCustomCommandPageViewModel:ViewModel
    {
        private long _hostId;
         private ValidatableObjectView<string> _name;
         private ValidatableObjectView<string> _commandString;
        public ICommand SaveCommand { get; }
         public ValidatableObjectView<string> Name
         {
             get => _name;
             set => SetProperty(ref _name, value);
         }

         public ValidatableObjectView<string> CommandString
        {
             get => _commandString;
             set => SetProperty(ref _commandString, value);
         }
        public AddCustomCommandPageViewModel()
         {
            Name = new ValidatableObjectView<string>(Resource.CommandName, true);
            CommandString = new ValidatableObjectView<string>(Resource.Command, true);
            AddValidations();
            SaveCommand =new Command(Save);
        }

        private async void Save(object obj)
        {
            var command = new CustomCommandSsh()
            {
                CommandString = _commandString.Value,
                Name = _name.Value
            };
            if (_hostId > 0) //TODO set ViewModel state or other flag 
            {
                command.Id = _hostId;
                await DataProvider.UpdateCustomCommandAsync(command).ConfigureAwait(false);
            }
            else
            {
                var id = await DataProvider.InsertCustomCommandAsync(command).ConfigureAwait(false);
               
            }
        }

        private void AddValidations()
        {
            _name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.NameToDisplayRequired
            });
            _commandString.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = Resource.NameToDisplayRequired
            });

        }
        public string IdQuery
        {
            set => _hostId = Int64.Parse(value);
        }
        public string NameQuery
        {
            set => _name.Value = Uri.UnescapeDataString(value);
        }

        public string CommandStringQuery
        {
            set => _commandString.Value = Uri.UnescapeDataString(value);
        }
    }
}
