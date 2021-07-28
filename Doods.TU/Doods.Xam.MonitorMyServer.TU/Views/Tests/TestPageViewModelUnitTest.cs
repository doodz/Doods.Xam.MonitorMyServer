using Doods.Xam.MonitorMyServer.Views.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Doods.Openmediavault.TU.Tests
{
    [TestClass]
    class TestPageViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new TestPageViewModel(null,null) ;

            Assert.IsNotNull(obj);


        }
    }
}
