using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.Xam.MonitorMyServer.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Data
{
    [TestClass]
    public class ZeroconfHostUnitTest : BaselUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new ZeroconfHost(null,null);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.IPAddresses);
            Assert.IsNull(obj.Services);
        }
    }
}
