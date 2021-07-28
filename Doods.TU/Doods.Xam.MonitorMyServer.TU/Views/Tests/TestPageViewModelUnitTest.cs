using Doods.Xam.MonitorMyServer.Views.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Doods.Openmediavault.TU.Tests
{
    [TestClass]
    public class TestPageViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            TestPageViewModel obj =null;
            Assert.ThrowsException<System.Exception>(() =>
            {
                obj = new TestPageViewModel(null, null);
            });
          

            Assert.IsNotNull(obj);


        }
    }
}
