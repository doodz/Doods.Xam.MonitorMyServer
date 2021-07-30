using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.Xam.MonitorMyServer.Views.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Views.All.Login
{
    [TestClass]
    public class LoginPageViewModelUnitTest : ViewModelUnitTest
    {
        [TestMethod]
        public void Create()
        {
           
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
          Assert.IsNotNull(obj.DisplayName);
          Assert.IsNotNull(obj.HostName);
          Assert.IsNotNull(obj.Login);
          Assert.IsNotNull(obj.Password);
          Assert.IsNotNull(obj.Port);

          Assert.IsNotNull(obj.ValidateUserNameCommand);
          Assert.IsNotNull(obj.TestMe);
          Assert.IsNotNull(obj.ValidateHostNameCommand);
          Assert.IsNotNull(obj.ValidatePasswordCommand);

       
        Assert.IsFalse(obj.IsOmvServer);
        Assert.IsFalse(obj.IsRpi);
        Assert.IsFalse(obj.IsSsh);
        Assert.IsFalse(obj.IsSynoServer);
        Assert.IsFalse(obj.IsWebminServer);
    }

        [TestMethod]
        public void ValidateConfig_False()
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.DisplayName);
            Assert.IsNotNull(obj.HostName);
            Assert.IsNotNull(obj.Login);
            Assert.IsNotNull(obj.Password);
            Assert.IsNotNull(obj.Port);

            Assert.IsNotNull(obj.ValidateUserNameCommand);
            Assert.IsNotNull(obj.TestMe);
            Assert.IsNotNull(obj.ValidateHostNameCommand);
            Assert.IsNotNull(obj.ValidatePasswordCommand);


            Assert.IsTrue(obj.TestMe.CanExecute(null));
            obj.TestMe.Execute(null);


            Assert.IsFalse(obj.DisplayName.IsValid);
            Assert.IsFalse(obj.HostName.IsValid);
            Assert.IsFalse(obj.Login.IsValid);
            Assert.IsFalse(obj.Password.IsValid);
            Assert.IsFalse(obj.Port.IsValid);


            
        }

        [TestMethod]
        public void ValidateConfig_True()
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.DisplayName);
            Assert.IsNotNull(obj.HostName);
            Assert.IsNotNull(obj.Login);
            Assert.IsNotNull(obj.Password);
            Assert.IsNotNull(obj.Port);

            Assert.IsNotNull(obj.ValidateUserNameCommand);
            Assert.IsNotNull(obj.TestMe);
            Assert.IsNotNull(obj.ValidateHostNameCommand);
            Assert.IsNotNull(obj.ValidatePasswordCommand);


            obj.HostName.Value = "doods.tu";
            obj.DisplayName.Value = "My test";
            obj.Login.Value = "doods";
            obj.Password.Value = "password";
            obj.Port.Value = "80";

            Assert.IsTrue(obj.TestMe.CanExecute(null));
            obj.TestMe.Execute(null);


            Assert.IsTrue(obj.DisplayName.IsValid);
            Assert.IsTrue(obj.HostName.IsValid);
            Assert.IsTrue(obj.Login.IsValid);
            Assert.IsTrue(obj.Password.IsValid);
            Assert.IsTrue(obj.Port.IsValid);
        }
    }
}
