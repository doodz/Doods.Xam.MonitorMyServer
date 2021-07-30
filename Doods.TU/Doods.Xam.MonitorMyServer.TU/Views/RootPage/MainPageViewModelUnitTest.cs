
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.RootPage
{
    [TestClass]
    public class MainPageViewModelUnitTest: ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var sshService = new Mock<ISshService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new MainPageViewModel(sshService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Groups);
            Assert.IsNull(obj.Hostnamectl);
            Assert.IsNull(obj.MemoryUsage);
            Assert.IsNull(obj.Processes);
            Assert.IsNull(obj.Upgradables);
           
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("toto")]
        public void GetBannerId(string myAdd)
        {

            var sshService = new Mock<ISshService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns(myAdd);
            var obj = new MainPageViewModel(sshService.Object, configurationMock.Object);
            Assert.IsNotNull(obj);
            Assert.AreEqual(myAdd, obj.BannerId);
        }

        [TestMethod]
        public void CheckTitleName()
        {
            var sshService = new Mock<ISshService>();
            var configurationMock = new Mock<Doods.Framework.Std.IConfiguration>();
            configurationMock.Setup(m => m.AdsKey).Returns("MyAdd");
            var obj = new MainPageViewModel(sshService.Object, configurationMock.Object);
            Assert.IsNotNull(obj.Title);
        }
    }
}
