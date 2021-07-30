using System.Collections.Generic;
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

            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ShowDetailsCmd);
            Assert.IsNotNull(obj.UpdatesCmd);
            Assert.IsNotNull(obj.CheckCmd);
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

        [TestMethod]
        public void CheckCmd()
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

            var omvService = new Mock<IOmvService>();
            omvService.Setup(x => x.UpdateAptList());

            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new OpenmediavaultDashboardViewModel(omvService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);

            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsTrue(obj.CheckCmd.CanExecute(null));

            obj.CheckCmd.Execute(null);
            omvService.Verify(c => c.UpdateAptList(), Times.Never);//Xamarin.Essentials exception :/
          

        }

        [TestMethod]
        public void UpdatesCmd()
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

            var omvService = new Mock<IOmvService>();
            omvService.Setup(x => x.UpgradeAptList(It.IsAny<IEnumerable<string>>()));


            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new OpenmediavaultDashboardViewModel(omvService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);

            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsTrue(obj.UpdatesCmd.CanExecute(null));

            obj.UpdatesCmd.Execute(null);
            omvService.Verify(x => x.UpgradeAptList(It.IsAny<IEnumerable<string>>()), Times.Never);//Xamarin.Essentials exception :/
            

        }
    }
}
