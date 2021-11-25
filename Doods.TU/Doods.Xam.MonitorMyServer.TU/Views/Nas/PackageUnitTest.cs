using Doods.Xam.MonitorMyServer.Data.Nas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Views.Nas
{
    [TestClass]
    public class PackageUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new Package();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Name);
            Assert.IsNull(obj.Desc);
            Assert.IsNull(obj.Status);
            Assert.IsNull(obj.Source);
            Assert.IsTrue(obj.IsSelected);
        }

        [TestMethod]
        public void SetDeviceName()
        {
            var deviceName = "you";
            var obj = new Package();
            obj.Name = deviceName;
            Assert.IsNotNull(obj);
            Assert.AreEqual(deviceName,obj.Name);
            Assert.IsNull(obj.Desc);
            Assert.IsNull(obj.Status);
            Assert.IsNull(obj.Source);
            Assert.IsTrue(obj.IsSelected);

        }
    }
}