using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.MobileMvvm
{
    [TestClass]
    public class BaseItemUnitTest
    {
       

        [TestMethod]
        public void Create()
        {
            //var loggertMock = new Mock<ILogger>();
            //var telemetryServiceMock = new Mock<ITelemetryService>();
            var viewModelMock = new Mock<IViewModel>();
            var obj = new ViewModelStateItem(viewModelMock.Object)as BaseItem;
            Assert.IsNotNull(obj);


            Assert.IsNull(obj.Icon);
            Assert.IsNull(obj.Title);
            Assert.IsNull(obj.Subtitle);
            Assert.IsNull(obj.Description);
            Assert.IsNull(obj.ToString());
            
        }

        [TestMethod]
        public void SetIcon()
        {
            //var loggertMock = new Mock<ILogger>();
            //var telemetryServiceMock = new Mock<ITelemetryService>();
            var viewModelMock = new Mock<IViewModel>();
            var obj = new ViewModelStateItem(viewModelMock.Object) as BaseItem;
            Assert.IsNotNull(obj);
            var propertyName = "";
           
            obj.PropertyChanged += (sender, args) =>
            {
                propertyName = args.PropertyName;
            };
            obj.Icon = "toto";
            Assert.IsNotNull(obj.Icon);
            Assert.IsNull(obj.Title);
            Assert.IsNull(obj.Subtitle);
            Assert.IsNull(obj.Description);
            Assert.IsNull(obj.ToString());

            Assert.AreEqual(nameof(BaseItem.Icon), propertyName);
        }

        [TestMethod]
        public void SetTitle()
        {
            //var loggertMock = new Mock<ILogger>();
            //var telemetryServiceMock = new Mock<ITelemetryService>();
            var viewModelMock = new Mock<IViewModel>();
            var obj = new ViewModelStateItem(viewModelMock.Object) as BaseItem;
            Assert.IsNotNull(obj);
            var propertyName = "";
           
            obj.PropertyChanged += (sender, args) =>
            {
                propertyName = args.PropertyName;
            };
            obj.Title = "toto";
            Assert.IsNull(obj.Icon);
            Assert.IsNotNull(obj.Title);
            Assert.IsNull(obj.Subtitle);
            Assert.IsNull(obj.Description);
            Assert.IsNotNull(obj.ToString());

            Assert.AreEqual(nameof(BaseItem.Title), propertyName);
        }
        [TestMethod]
        public void SetSubtitle()
        {
            //var loggertMock = new Mock<ILogger>();
            //var telemetryServiceMock = new Mock<ITelemetryService>();
            var viewModelMock = new Mock<IViewModel>();
            var obj = new ViewModelStateItem(viewModelMock.Object) as BaseItem;
            Assert.IsNotNull(obj);
            var propertyName = "";
          
            obj.PropertyChanged += (sender, args) =>
            {
                propertyName = args.PropertyName;
            };
            obj.Subtitle = "toto";
            Assert.IsNull(obj.Icon);
            Assert.IsNull(obj.Title);
            Assert.IsNotNull(obj.Subtitle);
            Assert.IsNull(obj.Description);
            Assert.IsNull(obj.ToString());

            Assert.AreEqual(nameof(BaseItem.Subtitle), propertyName);
        }
        [TestMethod]
        public void SetDescription()
        {
            //var loggertMock = new Mock<ILogger>();
            //var telemetryServiceMock = new Mock<ITelemetryService>();
            var viewModelMock = new Mock<IViewModel>();
            var obj = new ViewModelStateItem(viewModelMock.Object) as BaseItem;
            Assert.IsNotNull(obj);
            var propertyName = "";
           
            obj.PropertyChanged += (sender, args) =>
            {
                propertyName = args.PropertyName;
            };
            obj.Description = "toto";
            Assert.IsNull(obj.Icon);
            Assert.IsNull(obj.Title);
            Assert.IsNull(obj.Subtitle);
            Assert.IsNotNull(obj.Description);
            Assert.IsNull(obj.ToString());

            Assert.AreEqual(nameof(BaseItem.Description), propertyName);
        }
    }
}