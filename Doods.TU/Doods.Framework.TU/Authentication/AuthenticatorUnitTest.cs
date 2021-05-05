using Doods.Framework.ApiClientBase.Std.Authentication;
using Doods.Framework.Http.Std.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Authentication
{
    [TestClass]
    public class AuthenticatorUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var credentialsMock = new Mock<ICredentials>();
            var obj = new Authenticator(credentialsMock.Object);
            Assert.IsNotNull(obj);
           
        }
    }
}