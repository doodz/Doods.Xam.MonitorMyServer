using System.Threading.Tasks;
using Doods.Framework.Http.Std;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmedivault.Http.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Openmediavault.TU.Clients
{
    [TestClass]
    public class OmvDiskMgmtClientUnitTest
    {
        [TestMethod]
        public void Create_default()
        {
            var logger = new Mock<ILogger>();
            var client = new Mock<IHttpClient>();
            var rpc = new OmvHttpService(logger.Object, client.Object);
            var systemClient = new OmvDiskMgmtClient(rpc);
            Assert.IsNotNull(systemClient);
            Assert.AreSame(logger.Object, rpc.Logger);
        }

        [TestMethod]
        public async Task GetDisksBackground()
        {
            var logger = new Mock<ILogger>();
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvDiskMgmtClient = new OmvDiskMgmtClient(rpc);
            Assert.IsNotNull(omvDiskMgmtClient);
            var result = await omvDiskMgmtClient.GetDisksBackground();
            Assert.AreEqual("DiskMgmt_getListBg_result",result);

        }
    }
}