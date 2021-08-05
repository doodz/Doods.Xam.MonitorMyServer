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
    public class OmvAptClientUnitTest
    {
        [TestMethod]
        public void Create_default()
        {
            var logger = new Mock<ILogger>();
            var client = new Mock<IHttpClient>();
            var rpc = new OmvHttpService(logger.Object, client.Object);
            var systemClient = new OmvAptClient(rpc);
            Assert.IsNotNull(systemClient);
            Assert.AreSame(logger.Object, rpc.Logger);
        }

        [TestMethod]
        public async Task GetSettings()
        {
            var logger = new Mock<ILogger>();
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvAptClient = new OmvAptClient(rpc);
            Assert.IsNotNull(omvAptClient);
            var result =await omvAptClient.GetSettings();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetUpgraded()
        {
            var logger = new Mock<ILogger>();
            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvAptClient = new OmvAptClient(rpc);
            Assert.IsNotNull(omvAptClient);
            var result = await omvAptClient.GetUpgraded();
            Assert.IsNotNull(result);

        }
    }
}