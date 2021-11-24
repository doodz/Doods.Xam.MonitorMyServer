﻿using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.TU;
using Doods.Openmedivault.Http.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.All.EnumerateAllServicesFromAllHosts
{
    [TestClass]
    public class EnumerateAllServicesFromAllHostsViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var synologyCgiService = new Mock<IOmvService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new EnumerateAllServicesFromAllHostsViewModel();
            Assert.IsNotNull(obj);

           
        }

        [TestMethod]
        public async Task CallOnAppearingAsync2()
        {
            var logger = new Mock<ILogger>();
            var mapper = new Mock<IMapper>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();

            logger.SetupAllProperties();
            mapper.SetupAllProperties();
            configurationMock.SetupAllProperties();

            var rpc = new OmvHttpService(logger.Object, new LocalIHttpClient());
            var omvService = new OmvRpcService(rpc, logger.Object, mapper.Object);
            var obj = new EnumerateAllServicesFromAllHostsViewModel();

            Assert.IsNull(obj.Title);
            await obj.OnAppearingAsync();
          
        }
    }
}