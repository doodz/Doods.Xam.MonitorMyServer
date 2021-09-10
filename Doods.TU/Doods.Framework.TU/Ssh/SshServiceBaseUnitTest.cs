using System.Threading;
using System.Threading.Tasks;
using Doods.Framework.Ssh.Std;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Renci.SshNet;

namespace Doods.Framework.TU.Ssh
{
    internal class MySshServiceBase : SshServiceBase
    {
        public MySshServiceBase(ILogger logger) : base(logger)
        {
        }

        protected override SshClient GetSshClient()
        {
            return new SshClient(new ConnectionInfo("",""));
        }
    }


    [TestClass]
    public class SshServiceBaseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var logger = new Mock<ILogger>();
            var obj = (SshServiceBase) new MySshServiceBase(logger.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Client);
        }

        [TestMethod]
        public void GetLogger()
        {
            var logger = new Mock<ILogger>();
            var obj = (SshServiceBase)new MySshServiceBase(logger.Object);
            Assert.IsNotNull(obj);
            Assert.AreEqual(logger.Object,obj.Logger);
        }

        [TestMethod]
        public async Task RunCommandAsync()
        {
            var logger = new Mock<ILogger>();
            var obj = (SshServiceBase)new MySshServiceBase(logger.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Client);
            Assert.AreEqual(logger.Object, obj.Logger);
            var result = await obj.RunCommandAsync(null, CancellationToken.None);
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task ConnectAsync()
        {
            var logger = new Mock<ILogger>();
            var obj = (SshServiceBase)new MySshServiceBase(logger.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Client);
            Assert.AreEqual(logger.Object, obj.Logger);
            Assert.IsFalse(obj.IsConnected());
            await obj.ConnectAsync();
           
            var result = await obj.RunCommandAsync(null, CancellationToken.None);
            Assert.IsNull(result);

        }
    }
}