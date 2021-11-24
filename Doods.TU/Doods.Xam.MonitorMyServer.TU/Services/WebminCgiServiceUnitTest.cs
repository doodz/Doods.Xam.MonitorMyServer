using AutoMapper;
using Doods.Framework.ApiClientBase.Std.Authentication;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Std;
using Doods.Synology.Webapi.Std;
using Doods.Xam.MonitorMyServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Services
{
    [TestClass]
    public class WebminCgiServiceUnitTest : PackageUpdatesUnitTest
    {

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();
            var connectionMock = new Mock<IConnection>();
            connectionMock.Setup(connection => connection.Host).Returns("http://gbdtc");
            connectionMock.Setup(connection => connection.Port).Returns(42);
            connectionMock.Setup(connection => connection.ConnectionType).Returns(ConnectionType.Http);
            connectionMock.Setup(connection => connection.Credentials).Returns(new Credentials());
            _service = new WebminCgiService(loggerMock.Object, connectionMock.Object, mapperMock.Object);
            //Assert.IsInstanceOfType(_service, typeof(IPackageUpdates));
        }

        [TestMethod]
        public void Create()
        {

            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();
            var connectionMock = new Mock<IConnection>();
            connectionMock.Setup(connection => connection.Host).Returns("http://gbdtc");
            connectionMock.Setup(connection => connection.Port).Returns(42);
            connectionMock.Setup(connection => connection.ConnectionType).Returns(ConnectionType.Http);
            connectionMock.Setup(connection => connection.Credentials).Returns(new Credentials());
            var obj = new WebminCgiService(loggerMock.Object, connectionMock.Object, mapperMock.Object);
            Assert.IsInstanceOfType(obj, typeof(IWebminCgiService));

        }
    }
}