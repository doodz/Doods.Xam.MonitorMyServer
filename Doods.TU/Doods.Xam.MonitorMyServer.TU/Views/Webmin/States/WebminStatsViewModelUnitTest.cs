using Doods.Synology.Webapi.Std;
using Doods.Xam.MonitorMyServer.TU.Views.Base;
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
