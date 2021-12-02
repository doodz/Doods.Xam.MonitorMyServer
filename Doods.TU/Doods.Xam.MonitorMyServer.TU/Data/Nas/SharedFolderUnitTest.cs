using Doods.Xam.MonitorMyServer.Data.Nas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Views.Nas
{
    [TestClass]
    public class SharedFolderUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new SharedFolder();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Name);
            Assert.IsNull(obj.Description);
            Assert.IsNull(obj.Type);
            Assert.IsNull(obj.Volume);
            Assert.IsNotNull(obj.Uuid);
          
        }
    }
}