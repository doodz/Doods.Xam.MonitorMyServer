﻿using Doods.Framework.Std;

namespace Doods.Framework.Mobile.Ssh.Std.Models
{
    public class Upgradable : NotifyPropertyChangedBase
    {
        private string _name;
        private string _newVersion;
        private string _holdHold;
        private string _fromRepo;
        private string _platform;

        public string Name
        {
            get => _name;
            internal set => SetProperty(ref _name, value);
        }

        public string NewVersion
        {
            get => _newVersion;
            internal set => SetProperty(ref _newVersion, value);
        }
        public string HoldHold
        {
            get => _holdHold;
            internal set => SetProperty(ref _holdHold, value);
        }
        public string FromRepo
        {
            get => _fromRepo;
            internal set => SetProperty(ref _fromRepo, value);
        }
        public string Platform
        {
            get => _platform;
            internal set => SetProperty(ref _platform, value);
        }
    }
}