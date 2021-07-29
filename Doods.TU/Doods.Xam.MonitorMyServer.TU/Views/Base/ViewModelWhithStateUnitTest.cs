using Doods.Xam.MonitorMyServer.Views.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU
{
    [TestClass]
    public class ViewModelWhithStateUnitTest
    {
       [TestMethod]
        public void Create()
        {
            var obj = new ViewModelWhithState();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ViewModelStateItem);
        }
    }
}