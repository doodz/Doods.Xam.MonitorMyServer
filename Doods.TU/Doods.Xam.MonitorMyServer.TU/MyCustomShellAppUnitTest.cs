using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU
{
    [TestClass]
    public class MyCustomShellAppUnitTest : BaselUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new MyCustomShellApp();
            Assert.IsNotNull(obj);
        }
    }
}
