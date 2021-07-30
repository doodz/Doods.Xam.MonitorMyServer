using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.OpenMediaVault
{
    [TestClass]
    public class OpenmediavaultDashboardViewModelInutTest: ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var synologyCgiService = new Mock<IOmvService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new OpenmediavaultDashboardViewModel(synologyCgiService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
          
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("toto")]
        public void GetBannerId(string myAdd)
        {

            var synologyCgiService = new Mock<IOmvService>();
            var configurationMock = new Mock<Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns(myAdd);
            var obj = new OpenmediavaultDashboardViewModel(synologyCgiService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.AreEqual(myAdd, obj.BannerId);
        }

        [TestMethod]
        public void ChangeHostCmd()
        {
            bool called = false;

            var mockA = new Mock<IConnctionService>();
            mockA.Setup(x => x.ChangeHostTask()).Returns(() =>
            {
                called = true;
                return Task.FromResult(true);
            }
            );


            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA));
            SetMockContainer();

            var webminServiceMock = new Mock<IOmvService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new OpenmediavaultDashboardViewModel(webminServiceMock.Object, configurationMock.Object);
            Assert.IsNotNull(obj);

            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsTrue(obj.ChangeHostCmd.CanExecute(null));

            obj.ChangeHostCmd.Execute(null);
            mockA.Verify(c => c.ChangeHostTask(), Times.Once);
            Assert.IsTrue(called);

        }
    }
}
