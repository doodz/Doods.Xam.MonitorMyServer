using System.ComponentModel;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Behaviors
{
    public class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged
    {
        public ImageSource ShowIcon { get; set; }
        public ImageSource HideIcon { get; set; }

        private bool _hidePassword = true;

        public bool HidePassword
        {
            set
            {
                if (_hidePassword == value) return;
                _hidePassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HidePassword)));
            }
            get => _hidePassword;
        }

        protected override void Invoke(ImageButton sender)
        {



            HidePassword = !HidePassword; sender.Source = HidePassword ? HideIcon : ShowIcon;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}