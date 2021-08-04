using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Openmediavault.TU.Clients
{
    [TestClass]
    public class SshServiceBaseUnitTest
    {
        [TestMethod]
        public void Create_default()
        {
            var logger = new Mock<ILogger>();
            var connection = new Mock<IConnection>();
            var obj = new Openmedivault.Ssh.Std.OmvRpc(logger.Object, connection.Object);
            Assert.IsNotNull(obj);
            Assert.AreSame(logger.Object,obj.Logger);
            Assert.IsNull(obj.Client);
            Assert.IsNotNull(obj.ReadLock);
        }

        [TestMethod]
        public void IsConnected_false()
        {
            var logger = new Mock<ILogger>();
            var connection = new Mock<IConnection>();
            var obj = new Openmedivault.Ssh.Std.OmvRpc(logger.Object, connection.Object);
            Assert.IsNotNull(obj);
            Assert.AreSame(logger.Object, obj.Logger);
            Assert.IsNull(obj.Client);
            Assert.IsNotNull(obj.ReadLock);
            Assert.IsFalse(obj.IsConnected());
        }

        [TestMethod]
        public void IsAuthenticated_false()
        {
            var logger = new Mock<ILogger>();
            var connection = new Mock<IConnection>();
            var obj = new Openmedivault.Ssh.Std.OmvRpc(logger.Object, connection.Object);
            Assert.IsNotNull(obj);
            Assert.AreSame(logger.Object, obj.Logger);
            Assert.IsNull(obj.Client);
            Assert.IsNotNull(obj.ReadLock);
            Assert.IsFalse(obj.IsAuthenticated());
        }
    }
}