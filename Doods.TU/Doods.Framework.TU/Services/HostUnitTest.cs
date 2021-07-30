using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Services
{
    [TestClass]
    public class HostUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new Doods.Framework.Std.Services.Host();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Ip);
            Assert.IsNull(obj.Name);
            Assert.IsNotNull(obj.RoundtripTime);
            Assert.AreEqual(default,obj.RoundtripTime);
           
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("toto")]
        public void TestIp(string ip)
        {
            var obj = new Doods.Framework.Std.Services.Host();
            Assert.IsNotNull(obj);
            obj.Ip = ip;
            Assert.AreEqual(ip, obj.Ip);
            Assert.IsNull(obj.Name);
            Assert.IsNotNull(obj.RoundtripTime);
            Assert.AreEqual(default, obj.RoundtripTime);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("toto")]
        public void TestName(string name)
        {
            var obj = new Doods.Framework.Std.Services.Host();
            Assert.IsNotNull(obj);
            obj.Name = name;
            Assert.AreEqual(name, obj.Name);
            Assert.IsNull(obj.Ip);
            
            Assert.IsNotNull(obj.RoundtripTime);
            Assert.AreEqual(default, obj.RoundtripTime);
        }

        [DataTestMethod]
        [DataRow(long.MinValue)]
        [DataRow(long.MaxValue)]
        [DataRow(0L)]
        [DataRow(42L)]
        [DataRow(-42L)]
        public void TestRoundtripTime(long roundtripTime)
        {
            var obj = new Doods.Framework.Std.Services.Host();
            obj.RoundtripTime = roundtripTime;
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Ip);
            Assert.IsNull(obj.Name);
            Assert.IsNotNull(obj.RoundtripTime);
            Assert.AreEqual(roundtripTime, obj.RoundtripTime);
        }
    }
}
