using Autofac;
using Doods.Xam.MonitorMyServer;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.TU;
using Doods.Xam.MonitorMyServer.Views.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Openmediavault.TU.Tests
{
    [TestClass]
    public class TestPageViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var rewardService = new Mock<IRewardService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
           
            TestPageViewModel obj = new TestPageViewModel(rewardService.Object, configurationMock.Object); 

            
        }
    }
}
