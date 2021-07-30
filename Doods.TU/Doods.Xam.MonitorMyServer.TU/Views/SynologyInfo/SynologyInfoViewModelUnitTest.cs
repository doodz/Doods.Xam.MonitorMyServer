using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.SynologyInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.SynologyInfo
{
    [TestClass]
    public class SynologyInfoViewModelUnitTest: ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var synologyCgiService = new Mock<ISynologyCgiService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new SynologyInfoViewModel(synologyCgiService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.NetworkInfo);
            Assert.IsNull(obj.ServicesInfo);
            Assert.IsNull(obj.StorageInfo);
            Assert.IsNull(obj.SystemInfo);
            Assert.IsNull(obj.UpgradeStatus);
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("toto")]
        public void GetBannerId(string myAdd)
        {

            var synologyCgiService = new Mock<ISynologyCgiService>();
            var configurationMock = new Mock<Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns(myAdd);
            var obj = new SynologyInfoViewModel(synologyCgiService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.AreEqual(myAdd, obj.BannerId);
        }
    }
}
