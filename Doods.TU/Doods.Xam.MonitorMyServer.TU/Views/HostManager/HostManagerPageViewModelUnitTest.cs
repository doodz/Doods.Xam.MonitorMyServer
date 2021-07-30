using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Doods.Framework.Repository.Std.Tables;
using Doods.Synology.Webapi.Std;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.HostManager
{
    [TestClass]
    public class HostManagerPageViewModelUnitTest: ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
          
            var obj = new HostManagerPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Title);
            Assert.IsNotNull(obj.ItemsView);
            Assert.IsNotNull(obj.SelectItemCommand);
        }

        [TestMethod]
        public void SelectItemCommand_null()
        {
            var mockA = new Mock<IConnctionService>();
            mockA.Setup(x => x.SelectHost(It.IsAny<Host>()));
            //Doods.Framework.Repository.Std.Tables.Host
         

            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA));
            SetMockContainer();
            var obj = new HostManagerPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Title);
            Assert.IsNotNull(obj.ItemsView);
            Assert.IsNotNull(obj.SelectItemCommand);

            Assert.IsTrue(obj.SelectItemCommand.CanExecute(null));
            obj.SelectItemCommand.Execute(null);
            mockA.Verify(x => x.SelectHost(It.IsAny<Host>()), Times.Never());
            
        }

        [TestMethod]
        public void SelectItemCommand_HostViewModel()
        {
            var mockA = new Mock<IConnctionService>();
            mockA.Setup(x => x.SelectHost(It.IsAny<Host>()));
            //Doods.Framework.Repository.Std.Tables.Host


            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA));
            SetMockContainer();
            var obj = new HostManagerPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Title);
            Assert.IsNotNull(obj.ItemsView);
            Assert.IsNotNull(obj.SelectItemCommand);

            var vm = new HostViewModel(new Host());
            Assert.IsTrue(obj.SelectItemCommand.CanExecute(vm));
            obj.SelectItemCommand.Execute(vm);

            mockA.Verify(x => x.SelectHost(It.IsAny<Host>()), Times.Once());
        }
    }
}
