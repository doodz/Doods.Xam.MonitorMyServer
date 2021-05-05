using Doods.Framework.Ssh.Std.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Ssh.Converters
{
    [TestClass]
    public class SshToHostnamectlConverterUnitTest
    {
        [TestMethod]
        public void Create()
        {

            var obj = new SshToHostnamectlConverter();
            Assert.IsNotNull(obj);

        }
    }
}