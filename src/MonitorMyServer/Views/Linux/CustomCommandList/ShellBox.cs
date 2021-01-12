using System;
using System.Windows.Input;
using Doods.Framework.Ssh.Std;
using Doods.Framework.Std;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.CustomCommandList
{
    public sealed class ShellBox : NotifyPropertyChangedBase, IObserver<string>
    {
        private readonly ShellClient _shellClient;
        private string _output;
        private IDisposable _receiver;
        private string _sendText;

        public ShellBox(ShellClient shellClient)
        {
            _shellClient = shellClient;
            ExecuteCommand = new Command(Execute);
            _receiver =_shellClient.SubscribeTextReceived(this);
        }

        public ICommand ExecuteCommand { get; }

        public string Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        public string SendText
        {
            get => _sendText;
            set => SetProperty(ref _sendText, value);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(string value)
        {
            AddText(value);
        }

        private void Execute()
        {
            _shellClient.SendText(SendText);
            SendText = string.Empty;
        }

        public void Execute(string text)
        {
            SendText = text;
            Execute();
        }

        private void AddText(string text)
        {
            Output += text;
        }
        public void Subscribe()
        {
            Output = string.Empty;
            _receiver = _shellClient.SubscribeTextReceived(this);
        }
        public void Unsubscribe()
        {
            _receiver?.Dispose();
        }

        public void Clear()
        {
            Output = string.Empty;
        }
    }
}