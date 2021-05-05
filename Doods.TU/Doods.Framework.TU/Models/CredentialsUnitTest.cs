using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doods.Framework.ApiClientBase.Std.Authentication;

namespace Doods.Framework.TU
{
    [TestClass]
    public class CredentialsUnitTest
    {
        [TestMethod]
        public void Create_Anonymous_Credential()
        {
            var obj = new Credentials();

            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Login);
            Assert.IsNull(obj.Password);
            Assert.AreEqual(AuthenticationType.Anonymous, obj.AuthenticationType);
           
        }

        [TestMethod]
        public void Create_Simple_Credential()
        {
            var login = "foo";
            var password = "bar";
            var obj = new Credentials(login, password);

            Assert.IsNotNull(obj);
            Assert.AreEqual(login,obj.Login);
            Assert.AreEqual(password,obj.Password);
            Assert.AreEqual(AuthenticationType.Simple, obj.AuthenticationType);

        }

        [TestMethod]
        public void Create_OAuth_Credential()
        {
            var token = "123_token";
            var obj = new Credentials(token);

            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Login);
            Assert.AreEqual(token,obj.Password);
            Assert.AreEqual(AuthenticationType.OAuth, obj.AuthenticationType);

        }
    }
}
