using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.TU;
using Doods.Openmedivault.Http.Std;
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

        [TestMethod]
        public async Task CallOnAppearingAsync2()
        {
            var logger = new Mock<ILogger>();
            var mapper = new Mock<IMapper>();
            var messageBoxService = new Mock<IMessageBoxService>();

            logger.SetupAllProperties();
            mapper.SetupAllProperties();
            messageBoxService.SetupAllProperties();

            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvService = new OmvRpcService(rpc, logger.Object, mapper.Object);
            var obj = new OpenmediavaultFileSystemsViewModel(omvService, messageBoxService.Object);

            Assert.IsNull(obj.Title);
            await obj.OnAppearingAsync();
            Assert.AreEqual(0,obj.Filesystems.Count);
            

        }
        
    }
}
