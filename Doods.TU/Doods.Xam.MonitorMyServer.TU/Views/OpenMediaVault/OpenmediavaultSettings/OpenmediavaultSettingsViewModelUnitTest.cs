using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using AutoMapper;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;
using Doods.Openmediavault.TU.Clients;
using Doods.Openmedivault.Http.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.OpenMediaVault.OpenmediavaultSettings
{
    [TestClass]
    public class OpenmediavaultSettingsViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var OmvServiceMock = new Mock<IOmvService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new OpenmediavaultSettingsViewModel(OmvServiceMock.Object);
            Assert.IsNotNull(obj);

         
        }


        [TestMethod]
        public async Task CallOnAppearingAsync2()
        {
            var logger = new Mock<ILogger>();
            var mapper = new Mock<IMapper>();
            var configurationMock = new Mock<IConfiguration>();

            logger.SetupAllProperties();
            mapper.SetupAllProperties();
            configurationMock.SetupAllProperties();

            var mockA = new Mock<IConnctionService>();
            mockA.Setup(x => x.SelectHost(It.IsAny<Host>()));
            //Doods.Framework.Repository.Std.Tables.Host
            var mockB = new Mock<IOmvService>();
            mockB.Setup(x => x.GetWebGuiSettings()).ReturnsAsync(new WebGuiSetting());
            mockB.Setup(x => x.GetPowerManagementSetting()).ReturnsAsync(new PowerManagementSetting());
            mockB.Setup(x => x.GetDateAndTimeSetting()).ReturnsAsync(new TimeSetting(){Date = new Date()});
            mockB.Setup(x => x.GetAptSettings()).ReturnsAsync(new AptSetting());
            mockB.Setup(x => x.GetNetworkSetting()).ReturnsAsync(new NetworkSetting());
            
            
            //Doods.Framework.Repository.Std.Tables.Host

            mockA.SetupAllProperties();
            mockB.SetupAllProperties();
            

             LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(mockA);
                cfg.RegisterMock(mockB);
            });
            SetMockContainer();

            //var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            //var omvService = new OmvRpcService(rpc, logger.Object, mapper.Object);
            //var obj = new OpenmediavaultSettingsViewModel(omvService);
            var obj = new OpenmediavaultSettingsViewModel(mockB.Object);
            Assert.IsNull(obj.Title);
            await obj.OnAppearingAsync();
           
        }
    }
}
