using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Data
{
    [TestClass]
    public class SynchronizedCacheItemQueryShellUnitTest: BaselUnitTest
    {

        [TestMethod]
        public void Create()
        {
            var obj = new SynchronizedCacheItemQueryShell("null");

            Assert.IsNotNull(obj);
            Assert.AreEqual("CacheKeyQuery=SynchronizedCacheItemQueryShell&TypeNameQuery=String", obj.ToQuery());
           
        }


        [TestMethod]
        public void Create_IQueryShellNavigationObject()
        {
            var queryShellNavigationObject = new Mock<IQueryShellNavigationObject>();
            queryShellNavigationObject.Setup(o => o.ToQuery()).Returns(() => "IQueryShellNavigationObject_toto");
            var obj = new SynchronizedCacheItemQueryShell(queryShellNavigationObject.Object);

            Assert.IsNotNull(obj);
            Assert.AreEqual("IQueryShellNavigationObject_toto&CacheKeyQuery=SynchronizedCacheItemQueryShell&TypeNameQuery=IQueryShellNavigationObjectProxy", obj.ToQuery());

        }
    }
}
