using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.Framework.Std.Validation;
using Doods.Xam.MonitorMyServer.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Data
{
    [TestClass]
    public class DataHostUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new DataHost();

            Assert.IsNotNull(obj);

            Assert.IsNull(obj.DisplayName);
            Assert.IsNull(obj.IPAddress);
            Assert.IsNull(obj.Id);
        }


        [TestMethod]
        public void SetId()
        {
            var obj = new DataHost();
            obj.Id = "toto";
            Assert.IsNotNull(obj);

            Assert.IsNull(obj.DisplayName);
            Assert.IsNull(obj.IPAddress);
            Assert.AreEqual("toto",obj.Id);
        }

        [TestMethod]
        public void SetIPAddress()
        {
            var obj = new DataHost();
            obj.IPAddress = "toto";
            Assert.IsNotNull(obj);

            Assert.IsNull(obj.DisplayName);
            Assert.AreEqual("toto",obj.IPAddress);
            Assert.IsNull(obj.Id);
        }
        [TestMethod]
        public void SetDisplayName()
        {
            var obj = new DataHost();
            obj.DisplayName = "toto";
            Assert.IsNotNull(obj);

            Assert.AreEqual("toto",obj.DisplayName);
            Assert.IsNull(obj.IPAddress);
            Assert.IsNull(obj.Id);
        }
    }
}
