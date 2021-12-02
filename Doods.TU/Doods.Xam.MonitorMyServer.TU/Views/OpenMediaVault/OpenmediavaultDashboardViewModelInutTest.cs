using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using AutoMapper;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.TU.Clients;
using Doods.Openmediavault.Rpc.Std.TU;
using Doods.Openmedivault.Http.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.TU.Views.OpenMediaVault
{
    [TestClass]
    public class OpenmediavaultDashboardViewModelInutTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var synologyCgiService = new Mock<IOmvService>();
            var configurationMock = new Mock<IConfiguration>();
            var messagingCenterMock = new Mock<IMessagingCenter>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");

             //Doods.Framework.Repository.Std.Tables.Host
            var mockB = new Mock<IOmvService>();
            
            //Doods.Framework.Repository.Std.Tables.Host

            
            mockB.SetupAllProperties();
            mockB.Setup(x => x.GetOutput<string>(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Output<string>());


            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                
                cfg.RegisterMock(mockB);
                cfg.RegisterMock(messagingCenterMock);
                cfg.RegisterMock(configurationMock);
            });
            SetMockContainer();



            //var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            //var omvService = new OmvRpcService(rpc, logger.Object, mapper.Object);
            //var obj = new OpenmediavaultDashboardViewModel(omvService, configurationMock.Object);
            var obj = new OpenmediavaultDashboardViewModel(mockB.Object, configurationMock.Object, messagingCenterMock.Object);
            //var obj = new OpenmediavaultDashboardViewModel(synologyCgiService.Object, configurationMock.Object);

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
            var configurationMock = new Mock<IConfiguration>();
            var messagingCenterMock = new Mock<IMessagingCenter>();
            configurationMock.Setup(m => m.AdsKey).Returns(myAdd);

            //Doods.Framework.Repository.Std.Tables.Host
            var mockB = new Mock<IOmvService>();

            //Doods.Framework.Repository.Std.Tables.Host


            mockB.SetupAllProperties();
            mockB.Setup(x => x.GetOutput<string>(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Output<string>());


            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {

                cfg.RegisterMock(mockB);
                cfg.RegisterMock(messagingCenterMock);
            });
            SetMockContainer();
            var obj = new OpenmediavaultDashboardViewModel(mockB.Object, configurationMock.Object, messagingCenterMock.Object);
            //var obj = new OpenmediavaultDashboardViewModel(synologyCgiService.Object, configurationMock.Object);

            Assert.IsNotNull(obj);
            Assert.AreEqual(myAdd, obj.BannerId);
        }

        [TestMethod]
        public void ChangeHostCmd()
        {
            var called = false;

            var mockA = new Mock<IConnctionService>();
            mockA.Setup(x => x.ChangeHostTask()).Returns(() =>
                {
                    called = true;
                    return Task.FromResult(true);
                }
            );




            var messagingCenterMock = new Mock<IMessagingCenter>();
            var webminServiceMock = new Mock<IOmvService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd"); //Doods.Framework.Repository.Std.Tables.Host
            var mockB = new Mock<IOmvService>();

            //Doods.Framework.Repository.Std.Tables.Host


            mockB.SetupAllProperties();
            mockB.Setup(x => x.GetOutput<string>(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Output<string>());


            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(mockA);
                cfg.RegisterMock(mockB);
                cfg.RegisterMock(messagingCenterMock);
            });
            SetMockContainer();
            var obj = new OpenmediavaultDashboardViewModel(mockB.Object, configurationMock.Object, messagingCenterMock.Object);
            //var obj = new OpenmediavaultDashboardViewModel(synologyCgiService.Object, configurationMock.Object);

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
            var called = false;
            var messagingCenterMock = new Mock<IMessagingCenter>();
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
            omvService.Setup(x => x.GetOutput<string>(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Output<string>());

            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd"); //Doods.Framework.Repository.Std.Tables.Host
          

            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {

                cfg.RegisterMock(omvService);
                cfg.RegisterMock(messagingCenterMock);
            });
            SetMockContainer();
            var obj = new OpenmediavaultDashboardViewModel(omvService.Object, configurationMock.Object, messagingCenterMock.Object);
            //var obj = new OpenmediavaultDashboardViewModel(synologyCgiService.Object, configurationMock.Object);

            Assert.IsNotNull(obj);

            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsTrue(obj.CheckCmd.CanExecute(null));

            obj.CheckCmd.Execute(null);
            omvService.Verify(c => c.UpdateAptList(), Times.Once);
        }

        [TestMethod]
        public void UpdatesCmd()
        {
            var called = false;
            var messagingCenterMock = new Mock<IMessagingCenter>();
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
            omvService.Setup(x => x.GetOutput<string>(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Output<string>());


            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd"); //Doods.Framework.Repository.Std.Tables.Host
          
           

            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(messagingCenterMock);
                cfg.RegisterMock(omvService);
                
            });
            SetMockContainer();
            var obj = new OpenmediavaultDashboardViewModel(omvService.Object, configurationMock.Object, messagingCenterMock.Object);
            //var obj = new OpenmediavaultDashboardViewModel(synologyCgiService.Object, configurationMock.Object);

            Assert.IsNotNull(obj);

            Assert.IsNotNull(obj.ChangeHostCmd);
            Assert.IsNotNull(obj.ManageHostsCmd);
            Assert.IsTrue(obj.UpdatesCmd.CanExecute(null));

            obj.UpdatesCmd.Execute(null);
            omvService.Verify(x => x.UpgradeAptList(It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [TestMethod]
        public async Task CallOnAppearingAsync2()
        {
            var logger = new Mock<ILogger>();
            var mapper = new Mock<IMapper>();
            var configurationMock = new Mock<IConfiguration>();
            var messagingCenterMock = new Mock<IMessagingCenter>();
            logger.SetupAllProperties();
            mapper.SetupAllProperties();
            configurationMock.SetupAllProperties();
            //Doods.Framework.Repository.Std.Tables.Host
            var mockB = new Mock<IOmvService>();
            
            //Doods.Framework.Repository.Std.Tables.Host

            
            mockB.SetupAllProperties();
            mockB.Setup(x => x.GetOutput<string>(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new Output<string>());


            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(messagingCenterMock);
                cfg.RegisterMock(mockB);
                cfg.RegisterMock(configurationMock);
            });
            SetMockContainer();



            //var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            //var omvService = new OmvRpcService(rpc, logger.Object, mapper.Object);
            //var obj = new OpenmediavaultDashboardViewModel(omvService, configurationMock.Object);
            var obj = new OpenmediavaultDashboardViewModel(mockB.Object, configurationMock.Object, messagingCenterMock.Object);
            Assert.IsNull(obj.Title);
            await obj.OnAppearingAsync();
            Assert.AreEqual(0, obj.Filesystems.Count);

            Assert.IsNotNull(obj.Upgradeds);
            Assert.IsNotNull(obj.ServicesStatus);
            Assert.IsNotNull(obj.Filesystems);
            Assert.IsNotNull(obj.Devices);
            Assert.IsNull(obj.OMVInformations);
        }
    }
}