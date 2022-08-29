using System.Threading.Tasks;
using Doods.Framework.Http.Std;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmediavault.Rpc.Std.TU;
using Doods.Openmedivault.Http.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Openmediavault.TU.Clients
{
    [TestClass]
    public class OmvFileSystemMgmtClientUnitTest
    {
        [TestMethod]
        public void Create_default()
        {
            var logger = new Mock<ILogger>();
            var client = new Mock<IHttpClient>();
            var rpc = new OmvHttpService(logger.Object, client.Object);
            var systemClient = new OmvFileSystemMgmtClient(rpc);
            Assert.IsNotNull(systemClient);
            Assert.AreSame(logger.Object, rpc.Logger);
        }

        [TestMethod]
        public async Task GetCandidatesBg()
        {
            var logger = new Mock<ILogger>();
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvFileSystemMgmtClient = new OmvFileSystemMgmtClient(rpc);
            Assert.IsNotNull(omvFileSystemMgmtClient);
            var result = await omvFileSystemMgmtClient.GetCandidatesBg();
            Assert.AreEqual("FileSystemMgmt_getCandidatesBg_result", result);

        }

        [TestMethod]
        public async Task GetListFileSystemBackground()
        {
            var logger = new Mock<ILogger>();
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvFileSystemMgmtClient = new OmvFileSystemMgmtClient(rpc);
            Assert.IsNotNull(omvFileSystemMgmtClient);
            var result = await omvFileSystemMgmtClient.GetListFileSystemBackground();
            Assert.AreEqual("FileSystemMgmt_getListBg_result", result);

        }

        [TestMethod]
        public async Task GetMountCandidates()
        {
            var logger = new Mock<ILogger>();
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvFileSystemMgmtClient = new OmvFileSystemMgmtClient(rpc);
            Assert.IsNotNull(omvFileSystemMgmtClient);
            var result = await omvFileSystemMgmtClient.GetMountCandidates();
           Assert.IsNotNull(result);

        }


    }
}