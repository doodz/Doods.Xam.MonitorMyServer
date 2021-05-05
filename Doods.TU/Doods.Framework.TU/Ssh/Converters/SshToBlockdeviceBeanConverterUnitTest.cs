using Doods.Framework.Ssh.Std.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Ssh.Converters
{
    [TestClass]
    public class SshToBlockdeviceBeanConverterUnitTest
    {
        [TestMethod]
        public void Create()
        {

            var obj = new SshToBlockdeviceBeanConverter();
            Assert.IsNotNull(obj);

        }
    }
}