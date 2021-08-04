using System;
using System.Linq.Expressions;
using System.Reflection;
using Doods.Framework.Repository.Std.Tables;
using Doods.Xam.MonitorMyServer.Enums;
using Doods.Xam.MonitorMyServer.Views.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Views.All.Login
{
    public static class MyAttrubeExtensions
    {
        public static MethodInfo GetMethod<T>(this T instance,
            Expression<Func<T, object>> methodSelector)
        {
            // Note: this is a bit simplistic implementation. It will
            // not work for all expressions.
            return ((MethodCallExpression)methodSelector.Body).Method;
        }

        public static MethodInfo GetMethod<T>(this T instance,
            Expression<Action<T>> methodSelector)
        {
            return ((MethodCallExpression)methodSelector.Body).Method;
        }

        public static bool HasAttribute<TAttribute>(
            this MemberInfo member)
            where TAttribute : Attribute
        {
            return GetAttributes<TAttribute>(member).Length > 0;
        }

        public static TAttribute[] GetAttributes<TAttribute>(
            this MemberInfo member)
            where TAttribute : Attribute
        {
            var attributes =
                member.GetCustomAttributes(typeof(TAttribute), true);

            return (TAttribute[])attributes;
        }
    }


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

            Assert.IsNotNull(obj.CmdState);


            Assert.AreEqual((SupportedServicies)0
              , obj.TypeService);

            Assert.IsFalse(obj.IsOmvServer);
            Assert.IsFalse(obj.IsRpi);
            Assert.IsFalse(obj.IsSsh);
            Assert.IsFalse(obj.IsSynoServer);
            Assert.IsFalse(obj.IsWebminServer);
        }

        [TestMethod]
        public void HasAttributes()
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
           
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
            Assert.IsTrue(obj.Port.IsValid);



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

        [TestMethod]
        public void ValidateConfig_CmdState_False()
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.DisplayName);
            Assert.IsNotNull(obj.HostName);
            Assert.IsNotNull(obj.Login);
            Assert.IsNotNull(obj.Password);
            Assert.IsNotNull(obj.Port);

            Assert.IsNotNull(obj.ValidateUserNameCommand);
            Assert.IsNotNull(obj.CmdState);
            Assert.IsNotNull(obj.ValidateHostNameCommand);
            Assert.IsNotNull(obj.ValidatePasswordCommand);


            Assert.IsTrue(obj.CmdState.CanExecute(null));
            obj.CmdState.Execute(null);


            Assert.IsFalse(obj.DisplayName.IsValid);
            Assert.IsFalse(obj.HostName.IsValid);
            Assert.IsFalse(obj.Login.IsValid);
            Assert.IsFalse(obj.Password.IsValid);
            Assert.IsTrue(obj.Port.IsValid);



        }

        [DataTestMethod]
        [DataRow(SupportedServicies.NotDefined)]
        [DataRow(SupportedServicies.Openmediavault)]
        [DataRow(SupportedServicies.Openmediavault_SSH)]

        public void SetTypeService(SupportedServicies srv)
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);

            obj.TypeService = srv;
            Assert.AreEqual(srv
                , obj.TypeService);
        }


        [TestMethod]
        public void ValidateConfig_CmdState_True()
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.DisplayName);
            Assert.IsNotNull(obj.HostName);
            Assert.IsNotNull(obj.Login);
            Assert.IsNotNull(obj.Password);
            Assert.IsNotNull(obj.Port);

            Assert.IsNotNull(obj.ValidateUserNameCommand);
            Assert.IsNotNull(obj.CmdState);
            Assert.IsNotNull(obj.ValidateHostNameCommand);
            Assert.IsNotNull(obj.ValidatePasswordCommand);


            obj.HostName.Value = "doods.tu";
            obj.DisplayName.Value = "My test";
            obj.Login.Value = "doods";
            obj.Password.Value = "password";
            obj.Port.Value = "80";

            Assert.IsTrue(obj.CmdState.CanExecute(null));
            obj.CmdState.Execute(null);


            Assert.IsTrue(obj.DisplayName.IsValid);
            Assert.IsTrue(obj.HostName.IsValid);
            Assert.IsTrue(obj.Login.IsValid);
            Assert.IsTrue(obj.Password.IsValid);
            Assert.IsTrue(obj.Port.IsValid);
        }
        [TestMethod]
        public void SetHost_void()
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
            var host = new Host();
            obj.SetHost(host);


            Assert.AreEqual(obj.DisplayName.Value, host.HostName);
            Assert.AreEqual(obj.HostName.Value, host.Url);
            Assert.AreEqual(obj.Port.Value, host.Port.ToString());
            Assert.AreEqual(obj.Login.Value, host.UserName);
            Assert.AreEqual(obj.Password.Value, host.Password);

            Assert.AreEqual(obj.IsOmvServer, host.IsOmvServer);
            Assert.AreEqual(obj.IsRpi, host.IsRpi);
            Assert.AreEqual(obj.IsSsh, host.IsSsh);
            Assert.AreEqual(obj.IsSynoServer, host.IsSynoServer);
        }


        [TestMethod]
        public void SetHost()
        {
            var obj = new LoginPageViewModel();
            Assert.IsNotNull(obj);
            var host = new Host();
          

            host.HostName = "doods.tu";
            host.Url = "doods.url";
            host.Port = 80;
            host.UserName = "doods";
            host.Password = "password";
            host.Id = default;
            host.IsOmvServer = true;
            host.IsRpi = true;
            host.IsSsh = true;
            host.IsSynoServer = false;
            obj.SetHost(host);

            Assert.AreEqual(obj.DisplayName.Value, host.HostName);
            Assert.AreEqual(obj.HostName.Value, host.Url);
            Assert.AreEqual(obj.Port.Value, host.Port.ToString());
            Assert.AreEqual(obj.Login.Value, host.UserName);
            Assert.AreEqual(obj.Password.Value, host.Password);

            Assert.AreEqual(obj.IsOmvServer, host.IsOmvServer);
            Assert.AreEqual(obj.IsRpi, host.IsRpi);
            Assert.AreEqual(obj.IsSsh, host.IsSsh);
            Assert.AreEqual(obj.IsSynoServer, host.IsSynoServer);
        }
    }
}
