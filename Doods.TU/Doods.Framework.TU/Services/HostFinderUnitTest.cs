using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Services
{
    [TestClass]
    public class HostFinderUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var logger = new Mock<ILogger>();
            var obj = new Std.Services.HostFinder(logger.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ReachableAdress);

        }

        [TestMethod]
        public void SearchHosts()
        {
            var logger = new Mock<ILogger>();
            var obj = new Std.Services.HostFinder(logger.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ReachableAdress);

            obj.SearchHosts();


        }
    }
}
