using Doods.Framework.ApiClientBase.Std.Authentication;
using Doods.Framework.ApiClientBase.Std.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU
{
    [TestClass]
    public class SshConnectionUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var host = "doods.dev";
            var login = "foo";
            var password = "bar";

            var obj = new SshConnection(host,login,password);

            Assert.IsNotNull(obj);
            Assert.AreEqual(ConnectionType.Ssh, obj.ConnectionType);
            Assert.AreEqual(22, obj.Port);
            Assert.AreEqual(host, obj.Host);

            Assert.AreEqual(login, obj.Credentials.Login);
            Assert.AreEqual(password, obj.Credentials.Password);
            Assert.AreEqual(AuthenticationType.Simple, obj.Credentials.AuthenticationType);
        }
        [TestMethod]
        public void Create2()
        {
            var host = "doods.dev";
            var login = "foo";
            var password = "bar";
            var port = 42;
            var obj = new SshConnection(host, port,login, password);

            Assert.IsNotNull(obj);
            Assert.AreEqual(ConnectionType.Ssh, obj.ConnectionType);
            Assert.AreEqual(port, obj.Port);
            Assert.AreEqual(host, obj.Host);

            Assert.AreEqual(login, obj.Credentials.Login);
            Assert.AreEqual(password, obj.Credentials.Password);
            Assert.AreEqual(AuthenticationType.Simple, obj.Credentials.AuthenticationType);
        }
    }
}