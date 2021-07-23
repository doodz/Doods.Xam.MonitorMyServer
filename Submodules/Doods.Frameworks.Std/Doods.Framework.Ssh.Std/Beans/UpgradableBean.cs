using System.Collections.Generic;
using Doods.Framework.Std;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class UpgradableBeanWhapper : NotifyPropertyChangedBase
    {
        private ICollection<UpgradableBean> _upgradableBean;

        public UpgradableBeanWhapper(ICollection<UpgradableBean> upgradableBean)
        {
            UpgradableBean = upgradableBean;
        }

        public ICollection<UpgradableBean> UpgradableBean
        {
            get => _upgradableBean;
            internal set => SetProperty(ref _upgradableBean, value);
        }
    }

    public class UpgradableBean
    {
        public string Name { get; set; }
        public string NewVersion { get; set; }
        public string HoldHold { get; set; }
        public string FromRepo { get; set; }
        public string Platform { get; set; }
    }
}