using System;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Services
{
  
    public abstract class PackageUpdatesUnitTest
    {
        protected static IPackageUpdates _service;
        [TestMethod]
        public async Task IPackageUpdates_UpdatePackages_null_exception()
        {
            var rpcClientMock = new Mock<IRpcClient>();
            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();


            var obj = new OmvRpcService(rpcClientMock.Object, loggerMock.Object, mapperMock.Object);
            var result = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await _service.UpdatePackages(null);
            });
            Assert.AreEqual("packages", result.ParamName);
        }
    }
}