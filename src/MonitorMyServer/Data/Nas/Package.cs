using Doods.Framework.Std;

namespace Doods.Xam.MonitorMyServer.Data.Nas
{
    public class Package: NotifyPropertyChangedBase
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
        private bool _isSelected = true;
    }
}