using AutoMapper;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Services
{
    [TestClass]
    public class SshServiceUnitTest : PackageUpdatesUnitTest
    {

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();
            _service = new SshService(loggerMock.Object, mapperMock.Object);
            //Assert.IsInstanceOfType(_service, typeof(IPackageUpdates));
        }

        [TestMethod]
        public void Create()
        {

            var loggerMock = new Mock<ILogger>();
            var mapperMock = new Mock<IMapper>();

            var obj = new SshService(loggerMock.Object, mapperMock.Object);
            Assert.IsInstanceOfType(obj, typeof(ISshService));

        }
    }
}