using System.Threading.Tasks;
using Doods.Framework.Http.Std;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;
using Doods.Openmedivault.Http.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Openmediavault.Rpc.Std.TU
{
    [TestClass]
    public class OmvExecClientUnitTest
    {
        [TestMethod]
        public void Create_default()
        {
            var logger = new Mock<ILogger>();
            var client = new Mock<IHttpClient>();
            var rpc = new OmvHttpService(logger.Object, client.Object);
            var omvExecClient = new OmvExecClient(rpc);
            Assert.IsNotNull(omvExecClient);
            Assert.AreSame(logger.Object, rpc.Logger);
        }

        [TestMethod]
        public async Task GetDisksBackground()
        {
            var logger = new Mock<ILogger>();
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvExecClient = new OmvExecClient(rpc);
            Assert.IsNotNull(omvExecClient);
            var result = await omvExecClient.GetOutput<ResponseArray<Disk>>("DiskMgmt_getListBg_result",0);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsNotNull(result.Content.Data);
            Assert.AreEqual(2, result.Content.Data.Length);
        }
    }
}
