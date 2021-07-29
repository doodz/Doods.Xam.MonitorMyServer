using Autofac;
using Doods.Xam.MonitorMyServer;
using Doods.Xam.MonitorMyServer.TU.Views.Base;
using Doods.Xam.MonitorMyServer.Views.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Doods.Openmediavault.TU.Tests
{
    [TestClass]
    public class TestPageViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            TestPageViewModel obj =null;
            try
            {
                var builder = new ContainerBuilder();
                App.SetupContainer(builder);
                Assert.ThrowsException<System.Exception>(() => { obj = new TestPageViewModel(null, null); });

            }
            finally
            {

                Assert.IsNotNull(obj);
            }
        }
    }
}
