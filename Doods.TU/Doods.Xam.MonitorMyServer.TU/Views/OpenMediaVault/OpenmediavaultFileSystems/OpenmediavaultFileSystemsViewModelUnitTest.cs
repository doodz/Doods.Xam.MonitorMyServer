using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.OpenMediaVault.OpenmediavaultFileSystems
{
    [TestClass]
    public class OpenmediavaultFileSystemsViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var omvService = new Mock<IOmvService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new OpenmediavaultFileSystemsViewModel(omvService.Object, messageBoxService.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Filesystems);
        }

        [TestMethod]
        public void CheckTitleName()
        {
            var omvService = new Mock<IOmvService>();
            var messageBoxService = new Mock<IMessageBoxService>();
            var obj = new OpenmediavaultFileSystemsViewModel(omvService.Object, messageBoxService.Object);
            Assert.IsNull(obj.Title);
        }
    }
}
