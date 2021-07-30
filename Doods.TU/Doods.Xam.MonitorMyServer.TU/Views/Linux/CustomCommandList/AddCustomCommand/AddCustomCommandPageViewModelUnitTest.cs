using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.TU.Views.Base;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU.Views.Linux.CustomCommandList.AddCustomCommand
{

    [TestClass]
    public class AddCustomCommandPageViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
          
            var obj = new AddCustomCommandPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Name);
            Assert.IsNotNull(obj.CommandString);
            Assert.IsNotNull(obj.SaveCommand);

        }


        [TestMethod]
        public void Save()
        {

            var obj = new AddCustomCommandPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Name);
            Assert.IsNotNull(obj.CommandString);
            Assert.IsNotNull(obj.SaveCommand);
            Assert.IsTrue(obj.SaveCommand.CanExecute(null));

            obj.SaveCommand.Execute(null);
            Assert.IsFalse(obj.Name.IsValid);
            Assert.IsFalse(obj.CommandString.IsValid);
        }
    }
}
