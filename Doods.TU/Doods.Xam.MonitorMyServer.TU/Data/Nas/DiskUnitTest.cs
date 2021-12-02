using Doods.Xam.MonitorMyServer.Data.Nas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Views.Nas
{
    [TestClass]
    public class DiskUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new Disk();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.TotalSize);
            Assert.IsNull(obj.DeviceName);
            Assert.IsNull(obj.Vendor);
        }


        [TestMethod]
        public void SetTotalSize()
        {
            var zize = "123456";
            var obj = new Disk();
            Assert.IsNotNull(obj);
            obj.TotalSize = zize;
            Assert.AreEqual(zize, obj.TotalSize);
            Assert.IsNull(obj.DeviceName);
            Assert.IsNull(obj.Vendor);

        }

        [TestMethod]
        public void SetDeviceName()
        {
            var deviceName = "you";
            var obj = new Disk();
            Assert.IsNotNull(obj);
            obj.DeviceName = deviceName;
            Assert.IsNull(obj.TotalSize);
            Assert.AreEqual(deviceName,obj.DeviceName);
            Assert.IsNull(obj.Vendor);

        }

        [TestMethod]
        public void SetVendor()
        {
            var vendor = "me_doods";
            var obj = new Disk();
            Assert.IsNotNull(obj);
            obj.Vendor = vendor;
            Assert.IsNull(obj.TotalSize);
            Assert.IsNull(obj.DeviceName);
            Assert.AreEqual(vendor, obj.Vendor);

        }
    }
}
