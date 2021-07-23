using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Std.Models
{
    public abstract class BaseItem : NotifyPropertyChangedBase
    {
        private string _description;
        private string _icon;
        public string _subtitle;
        private string _title;

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Subtitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}