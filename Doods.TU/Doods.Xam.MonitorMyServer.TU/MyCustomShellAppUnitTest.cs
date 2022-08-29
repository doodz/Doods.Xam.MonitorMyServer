using System;
using Autofac;
using Autofac.Extras.Moq;
using Doods.Framework.Repository.Std.Tables;
using Doods.Xam.MonitorMyServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.TU
{
    [TestClass]
    public class MyCustomShellAppUnitTest : BaselUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new MyCustomShellApp();
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void HostChanged_Omv()
        {
            var messagingCenterMock = new Mock<IMessagingCenter>();

            Action<IConnctionService, Host> myAction = null;
            messagingCenterMock.Setup(s => 
                s.Subscribe(It.IsAny<object>(), It.IsAny<string>(),
                    It.IsAny<Action<IConnctionService,Host>>(),It.IsAny<IConnctionService>()))
                .Callback<object ,string , Action< IConnctionService, Host > ,IConnctionService>((o, s, arg3, arg4) =>
                {
                    myAction = arg3;
                } );
            //object subscriber,string message, Action< IConnctionService, Host > callback,IConnctionService source
            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(messagingCenterMock);
            });
            SetMockContainer();
            var obj = new MyCustomShellApp();
            Assert.IsNotNull(obj);
            var host = new Host
            {
                IsOmvServer = true
            };
            //void Subscribe<TSender, TArgs>(
            //    object subscriber,
            //    string message,
            //    Action<TSender, TArgs> callback,
            //    TSender source = null)
            //    where TSender : class;

            Assert.IsNotNull(myAction);
            myAction.Invoke(null, host);
            //messagingCenterMock.Object.Send(LocalAutoMock.Container.Resolve<IConnctionService>(), MessengerKeys.HostChanged, host);
        }


        [TestMethod]
        public void HostChanged_Syno()
        {
            var messagingCenterMock = new Mock<IMessagingCenter>();

            Action<IConnctionService, Host> myAction = null;
            messagingCenterMock.Setup(s =>
                    s.Subscribe(It.IsAny<object>(), It.IsAny<string>(),
                        It.IsAny<Action<IConnctionService, Host>>(), It.IsAny<IConnctionService>()))
                .Callback<object, string, Action<IConnctionService, Host>, IConnctionService>((o, s, arg3, arg4) =>
                {
                    myAction = arg3;
                });
            //object subscriber,string message, Action< IConnctionService, Host > callback,IConnctionService source
            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(messagingCenterMock);
            });
            SetMockContainer();
            var obj = new MyCustomShellApp();
            Assert.IsNotNull(obj);
            var host = new Host
            {
                IsSynoServer = true
            };
            Assert.IsNotNull(myAction);
            myAction.Invoke(null, host);
        }


        [TestMethod]
        public void HostChanged_Webmin()
        {
            var messagingCenterMock = new Mock<IMessagingCenter>();

            Action<IConnctionService, Host> myAction = null;
            messagingCenterMock.Setup(s =>
                    s.Subscribe(It.IsAny<object>(), It.IsAny<string>(),
                        It.IsAny<Action<IConnctionService, Host>>(), It.IsAny<IConnctionService>()))
                .Callback<object, string, Action<IConnctionService, Host>, IConnctionService>((o, s, arg3, arg4) =>
                {
                    myAction = arg3;
                });
            //object subscriber,string message, Action< IConnctionService, Host > callback,IConnctionService source
            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(messagingCenterMock);
            });
            SetMockContainer();
            var obj = new MyCustomShellApp();
            Assert.IsNotNull(obj);
            var host = new Host
            {
                IsWebminServer = true
            };
            Assert.IsNotNull(myAction);
            myAction.Invoke(null, host);
        }

        [TestMethod]
        public void HostChanged_Ssh()
        {
            var messagingCenterMock = new Mock<IMessagingCenter>();

            Action<IConnctionService, Host> myAction = null;
            messagingCenterMock.Setup(s =>
                    s.Subscribe(It.IsAny<object>(), It.IsAny<string>(),
                        It.IsAny<Action<IConnctionService, Host>>(), It.IsAny<IConnctionService>()))
                .Callback<object, string, Action<IConnctionService, Host>, IConnctionService>((o, s, arg3, arg4) =>
                {
                    myAction = arg3;
                });
            //object subscriber,string message, Action< IConnctionService, Host > callback,IConnctionService source
            LocalAutoMock = AutoMock.GetLoose(cfg =>
            {
                cfg.RegisterMock(messagingCenterMock);
            });
            SetMockContainer();
            var obj = new MyCustomShellApp();
            Assert.IsNotNull(obj);
            var host = new Host
            {
                IsSsh = true
            };
            Assert.IsNotNull(myAction);
            myAction.Invoke(null, host);
        }
    }
}
