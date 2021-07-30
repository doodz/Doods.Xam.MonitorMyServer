using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Doods.Framework.Mobile.Std.Config;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Synology.Webapi.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.TU.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.Webmin.States;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.Webmin.States
{
    [TestClass]
    public class WebminStatsViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var webminServiceMock = new Mock<IWebminCgiService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new WebminStatsViewModel(webminServiceMock.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Stats);
            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
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

            var webminServiceMock = new Mock<IWebminCgiService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new WebminStatsViewModel(webminServiceMock.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Stats);
            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsTrue(obj.ChangeHostCmd.CanExecute(null));

            obj.ChangeHostCmd.Execute(null);
            mockA.Verify(c=> c.ChangeHostTask(),Times.Once);
            Assert.IsTrue(called);

        }

        [TestMethod]
        public void ManageHostsCmd()
        {
            bool called = false;
            var callArgs = string.Empty;
            var mockA = new Mock<INavigationService>();
            mockA.Setup(x => x.NavigateAsync(It.IsAny<string>(),true))
                .Callback((string s,bool a) => callArgs=s)
                .Returns(() =>
                {
                    called = true;
                    return Task.FromResult(true);
                }
            );


            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA).Keyed<INavigationService>(NavigationServiceType.ShellNavigation).SingleInstance());
            SetMockContainer();

            var webminServiceMock = new Mock<IWebminCgiService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new WebminStatsViewModel(webminServiceMock.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Stats);
            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsTrue(obj.ChangeHostCmd.CanExecute(null));

            obj.ManageHostsCmd.Execute(null);
            mockA.Verify(c => c.NavigateAsync(It.IsAny<string>(), true), Times.Once);
            Assert.AreEqual(nameof(HostManagerPage),callArgs);
            Assert.IsTrue(called);

        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("toto")]
        public void GetBannerId(string myAdd)
        {
           
            var webminServiceMock = new Mock<IWebminCgiService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns(myAdd);
            var obj = new WebminStatsViewModel(webminServiceMock.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.AreEqual(myAdd,obj.BannerId);
        }
    }
}
