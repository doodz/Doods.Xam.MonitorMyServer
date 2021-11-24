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
    [TestClass]
    public class OmvRpcServiceUnitTest : PackageUpdatesUnitTest
    {

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            var rpcClientMock = new Mock<IRpcClient>();
            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();


            _service = new OmvRpcService(rpcClientMock.Object, loggerMock.Object, mapperMock.Object);
            //Assert.IsInstanceOfType(_service, typeof(IPackageUpdates));
        }
        [TestMethod]
        public void Create()
        {
            var rpcClientMock = new Mock<IRpcClient>();
            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();

           
            var obj = new OmvRpcService(rpcClientMock.Object, loggerMock.Object, mapperMock.Object);
            Assert.IsInstanceOfType(obj, typeof(IOmvService));
            
        }

        [TestMethod]
        public async Task GetPackages()
        {
            var rpcClientMock = new Mock<IRpcClient>();
            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();


            var obj = new OmvRpcService(rpcClientMock.Object, loggerMock.Object, mapperMock.Object);
            Assert.IsInstanceOfType(obj, typeof(IOmvService));
            await obj.GetPackages();
            

        }
        [TestMethod]
        public async Task UpdatePackages_null_exception()
        {
            var rpcClientMock = new Mock<IRpcClient>();
            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();


            var obj = new OmvRpcService(rpcClientMock.Object, loggerMock.Object, mapperMock.Object);
            var result = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await obj.UpdatePackages(null);
            });
           Assert.AreEqual("packages",result.ParamName);
        }
    }
}
