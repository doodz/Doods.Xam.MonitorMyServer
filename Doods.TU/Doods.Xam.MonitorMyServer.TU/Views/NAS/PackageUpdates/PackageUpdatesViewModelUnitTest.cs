using System.Threading.Tasks;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.NAS.PackageUpdates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.NAS
{
    [TestClass]
    public class PackageUpdatesViewModelUnitTest : ViewModelWhithStateUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var packageUpdates = new Mock<IPackageUpdates>();
            var obj = new PackageUpdatesViewModel(packageUpdates.Object);
            Assert.IsNotNull(obj.SelectAllCmd);
            Assert.IsNotNull(obj.UpdatePackagesCmd);
            Assert.IsNotNull(obj.InvertSelectCmd);
            Assert.IsNull(obj.Packages);
        }

        [TestMethod]
        public async Task CallOnAppearingAsync2()
        {
            var packageUpdates = new Mock<IPackageUpdates>();
            var obj = new PackageUpdatesViewModel(packageUpdates.Object);
            await obj.OnAppearingAsync();
        }

    }
}
