using Doods.Xam.MonitorMyServer.Data.Nas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Views.Nas
{
    [TestClass]
    public class FileSystemUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new FileSystem();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Device);
            Assert.IsNull(obj.Name);
            Assert.IsNull(obj.Size);
        }
        [TestMethod]
        public void SetSize()
        {
            var zize = "123456";
            var obj = new FileSystem();
            Assert.IsNotNull(obj);
            obj.Size = zize;
            Assert.AreEqual(zize, obj.Size);
            Assert.IsNull(obj.Name);
            Assert.IsNull(obj.Device);

        }

        [TestMethod]
        public void SetName()
        {
            var deviceName = "you";
            var obj = new FileSystem();
            Assert.IsNotNull(obj);
            obj.Name = deviceName;
            Assert.IsNull(obj.Size);
            Assert.AreEqual(deviceName, obj.Name);
            Assert.IsNull(obj.Device);

        }

        [TestMethod]
        public void SetDevice()
        {
            var vendor = "me_doods";
            var obj = new FileSystem();
            Assert.IsNotNull(obj);
            obj.Device = vendor;
            Assert.IsNull(obj.Size);
            Assert.IsNull(obj.Name);
            Assert.AreEqual(vendor, obj.Device);

        }
    }
}