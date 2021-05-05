using Doods.Framework.ApiClientBase.Std.Authentication;
using Doods.Framework.ApiClientBase.Std.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU
{
    [TestClass]
    public class HttpConnectionUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var host = "doods.dev";
            var port = 42;
            var obj = new HttpConnection(host, port);

            Assert.IsNotNull(obj);
            Assert.AreEqual(ConnectionType.Http, obj.ConnectionType);
            Assert.AreEqual(port, obj.Port);
            Assert.AreEqual(host, obj.Host);

            Assert.IsNull(obj.Credentials.Login);
            Assert.IsNull(obj.Credentials.Password);
            Assert.AreEqual(AuthenticationType.Anonymous, obj.Credentials.AuthenticationType);
        }
    }
}