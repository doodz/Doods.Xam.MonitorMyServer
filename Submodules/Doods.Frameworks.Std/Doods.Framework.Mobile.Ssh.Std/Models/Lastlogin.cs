using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Ssh.Std.Models
{
    public class Lastlogin : NotifyPropertyChangedBase
    {
        private string _date;
        private string _logedFrom;
        private string _logedIn;
        private string _logedOn;

        private string _stillLogged;
        private string _userName;

        public string UserName
        {
            get => _userName;
            internal set => SetProperty(ref _userName, value);
        }

        public string LogedOn
        {
            get => _logedOn;
            internal set => SetProperty(ref _logedOn, value);
        }

        public string Date
        {
            get => _date;
            internal set => SetProperty(ref _date, value);
        }

        public string StillLogged
        {
            get => _stillLogged;
            internal set => SetProperty(ref _stillLogged, value);
        }

        public string LogedIn
        {
            get => _logedIn;
            internal set => SetProperty(ref _logedIn, value);
        }

        public string LogedFrom
        {
            get => _logedFrom;
            internal set => SetProperty(ref _logedFrom, value);
        }
    }
}