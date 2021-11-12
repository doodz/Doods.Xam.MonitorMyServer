using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Doods.Framework.Mobile.Std.Config;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Zeroconf;

namespace Doods.Xam.MonitorMyServer.TU.Data
{
    [TestClass]
    public class ZeroconfHostUnitTest : BaselUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new ZeroconfHost(null,null);
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.IPAddresses);
            Assert.IsNull(obj.Services);
        }

        [TestMethod]
        public async Task CallGoToLoginCmd()
        {
         
            var mockB = new Mock<IService>();
            mockB.SetupAllProperties();
            bool called = false;
            var callArgs = string.Empty;
            var mockA = new Mock<INavigationService>();


            mockA.Setup(x => x.NavigateAsync(It.IsAny<string>(), true))
                .Callback((string s, bool a) => callArgs = s)
                .Returns(() =>
                    {
                        called = true;
                        return Task.FromResult(true);
                    }
                );


            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA).Keyed<INavigationService>(NavigationServiceType.ShellNavigation).SingleInstance());
            SetMockContainer();

            var token = new CancellationToken(false);
            var obj = new ZeroconfHost(new List<string>() { "local.net"}, new Dictionary<string, IService>() { {"", mockB.Object} });
            Assert.IsNotNull(obj);
            obj.IPAddress = "myhost.net";
            obj.GoToLoginCmd.Execute(null);

            mockA.Verify(x => x.NavigateAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()), Times.Once);

        }

        [DataTestMethod]
        [DataRow("toto@tutu.fr", "tutu.fr")]
        [DataRow("toto@tutu.com", "tutu.com")]
        [DataRow("toto.tutu@tata.fr", "tata.fr")]
        public async Task ToQueryTest(string displayName,string iPAddress)
        {

            var mockB = new Mock<IService>();
            mockB.SetupAllProperties();
            bool called = false;
            var callArgs = string.Empty;
            var mockA = new Mock<INavigationService>();


            mockA.Setup(x => x.NavigateAsync(It.IsAny<string>(), true))
                .Callback((string s, bool a) => callArgs = s)
                .Returns(() =>
                    {
                        called = true;
                        return Task.FromResult(true);
                    }
                );


            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA).Keyed<INavigationService>(NavigationServiceType.ShellNavigation).SingleInstance());
            SetMockContainer();

            var token = new CancellationToken(false);
            var obj = new ZeroconfHost(new List<string>() { "local.net" }, new Dictionary<string, IService>() { { "", mockB.Object } });
            Assert.IsNotNull(obj);
            obj.IPAddress = iPAddress;
            //obj.Port = port;
            obj.DisplayName = displayName;
            var str = obj.ToQuery();
            Assert.AreEqual($"DisplayNameQuery={displayName}&IPAddressQuery={Uri.EscapeDataString($"https://{iPAddress}")}",str);

        }

        [DataTestMethod]
        [DataRow("toto@tutu.fr", "http://tutu.fr")]
        [DataRow("toto@tutu.com", "http://tutu.com")]
        [DataRow("toto.tutu@tata.fr", "http://tata.fr")]
        public async Task ToQueryHttpTest(string displayName, string iPAddress)
        {

            var mockB = new Mock<IService>();
            mockB.SetupAllProperties();
            bool called = false;
            var callArgs = string.Empty;
            var mockA = new Mock<INavigationService>();


            mockA.Setup(x => x.NavigateAsync(It.IsAny<string>(), true))
                .Callback((string s, bool a) => callArgs = s)
                .Returns(() =>
                    {
                        called = true;
                        return Task.FromResult(true);
                    }
                );


            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA).Keyed<INavigationService>(NavigationServiceType.ShellNavigation).SingleInstance());
            SetMockContainer();

            var token = new CancellationToken(false);
            var obj = new ZeroconfHost(new List<string>() { "local.net" }, new Dictionary<string, IService>() { { "", mockB.Object } });
            Assert.IsNotNull(obj);
            obj.IPAddress = iPAddress;
            //obj.Port = port;
            obj.DisplayName = displayName;
            var str = obj.ToQuery();
            Assert.AreEqual($"DisplayNameQuery={displayName}&IPAddressQuery={Uri.EscapeDataString($"http://{iPAddress}")}", str);

        }

        [DataTestMethod]
        [DataRow("toto@tutu.fr", "tutu.fr",int.MaxValue)]
        [DataRow("toto@tutu.com", "tutu.com",int.MinValue)]
        [DataRow("toto.tutu@tata.fr", "tata.fr",0)]
        public async Task ToQuerySshTest(string displayName, string iPAddress,int port)
        {

            var mockB = new Mock<IService>();
            mockB.SetupAllProperties();
            mockB.Setup(service => service.Port).Returns(port);
            bool called = false;
            var callArgs = string.Empty;
            var mockA = new Mock<INavigationService>();


            mockA.Setup(x => x.NavigateAsync(It.IsAny<string>(), true))
                .Callback((string s, bool a) => callArgs = s)
                .Returns(() =>
                    {
                        called = true;
                        return Task.FromResult(true);
                    }
                );


            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA).Keyed<INavigationService>(NavigationServiceType.ShellNavigation).SingleInstance());
            SetMockContainer();

            var token = new CancellationToken(false);
            var obj = new ZeroconfHost(new List<string>() { "_ssh._tcp.local." }, new Dictionary<string, IService>() { { "_ssh._tcp.local.", mockB.Object } });
            Assert.IsNotNull(obj);
            obj.IPAddress = iPAddress;
            //obj.Port = port;
            obj.DisplayName = displayName;
            var str = obj.ToQuery();
            Assert.AreEqual($"DisplayNameQuery={displayName}&IPAddressQuery={iPAddress}&PortQuery={port}", str);

        }
    }
}
