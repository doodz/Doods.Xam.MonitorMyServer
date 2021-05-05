using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doods.Openmediavault.Mobile.Std.Enums;
using Doods.Openmediavault.Rpc.Std;
using Doods.Openmediavault.Rpc.Std.Clients;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmediavault.Rpc.std.Data.V5;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Openmediavault.TU.Clients
{
    [TestClass]
    public class OMVVersionsUnitTest
    {
        [DataTestMethod]
        [DataRow("NotFound")]
        [DataRow("Ix")]
        [DataRow("Omnius")]
        [DataRow("Fedaykin")]
        [DataRow("Sardaukar")]
        [DataRow("Kralizec")]
        [DataRow("Stone burner")]
        [DataRow("Erasmus")]
        [DataRow("Arrakis")]
        [DataRow("Usul")]
        public void Create(string version)
        {
            var v = OMVVersions.GetVersionFromString(version);
            Assert.IsNotNull(v);
            Assert.AreEqual(version, v.Name);
        }

        [TestMethod]
        public void NotFound()
        {
            var v = OMVVersions.GetVersionFromString("version");
            Assert.IsNotNull(v);
            Assert.AreEqual("NotFound", v.Name);
        }

        [TestMethod]
        public void Version4lowerThan5()
        {
            var b = (OMVVersions.Version4 < OMVVersions.Version5);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void Version4UpperThan2()
        {

            var b = (OMVVersions.Version4 > OMVVersions.Version2);
            Assert.IsTrue(b);
        }
    }


    [TestClass]
    public class OmvSystemClientUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var rpcClient = new Mock<IRpcClient>();
            var obj = new OmvSystemClient(rpcClient.Object);
            Assert.IsNotNull(obj);
        }
        [TestMethod]
        public async Task GetTimeZoneList()
        {
            var rpcClient = new Mock<IRpcClient>();
            //rpcClient.Setup(c => c.ExecuteTaskAsync<It.IsAnyType>()).ReturnsAsync(()=>default);
            var obj = new OmvSystemClient(rpcClient.Object);
            Assert.IsNotNull(obj);
            var result = await obj.GetTimeZoneList();
            Assert.IsNotNull(result);
            Assert.AreEqual(0,result.Count());
        }

        [TestMethod]
        public async Task GetTimeZoneList2()
        {
            var rpcClient = new Mock<IRpcClient>();
            rpcClient.Setup(c => c.ExecuteTaskAsync<IEnumerable<string>>(It.IsAny<IRpcRequest>()))
                .ReturnsAsync(new List<string>(){"foo","bar"});
            var obj = new OmvSystemClient(rpcClient.Object);
            Assert.IsNotNull(obj);
            var result = await obj.GetTimeZoneList();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetDateAndTimeSetting()
        {
            var rpcClient = new Mock<IRpcClient>();
            rpcClient.Setup(c => c.ExecuteTaskAsync<TimeSetting>(It.IsAny<IRpcRequest>()))
                .ReturnsAsync(new TimeSetting());
            var obj = new OmvSystemClient(rpcClient.Object);
            Assert.IsNotNull(obj);
            var result = await obj.GetDateAndTimeSetting();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SetDateAndTimeSetting()
        {
            var rpcClient = new Mock<IRpcClient>();
            var settings = new TimeSetting();
            rpcClient.Setup(c => c.ExecuteTaskAsync<object>(It.IsAny<IRpcRequest>()))
                .ReturnsAsync(new object());
            var obj = new OmvSystemClient(rpcClient.Object);
            Assert.IsNotNull(obj);
            var result = await obj.SetDateAndTimeSetting(settings);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetSystemInformationsV4()
        {
            var rpcClient = new Mock<IRpcClient>();
            var slt = new List<SystemInformation>();

            var info = new SystemInformation();
            info.Name = "ts";
            info.Value = (ValueUnion)"1000";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "System time";
            info.Value = (ValueUnion)"System_time_string";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "Hostname";
            info.Value = (ValueUnion)"Hostname_string";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "Version";
            info.Value = (ValueUnion)"Version_string";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "Processor";
            info.Value = (ValueUnion)"Processor_string";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "MemTotal";
            info.Value = (ValueUnion)"2000";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "Kernel";
            info.Value = (ValueUnion)"Kernel_string";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "Uptime";
            info.Value = (ValueUnion)"Uptime_string";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "Load average";
            info.Value = (ValueUnion)"Load_average_string";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "configDirty";
            info.Value = (ValueUnion)"true";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "rebootRequired";
            info.Value = (ValueUnion)"false";
            slt.Add(info);

            info = new SystemInformation();
            info.Name = "pkgUpdatesAvailable";
            info.Value = (ValueUnion)"true";
            slt.Add(info);


            //rpcClient.Setup(c => c.ExecuteTaskAsync<IEnumerable<SystemInformation>>(It.IsAny<IRpcRequest>()))
            //    .ReturnsAsync(slt);
            //rpcClient.Setup(c => c.ExecuteTaskAsync<IEnumerable<SystemInformation>>(It.IsAny<IRpcRequest>()))
            //    .ReturnsAsync(slt);
            rpcClient.Setup(c => c.ExecuteTaskAsync<object>(It.Is<IRpcRequest>(t => t.Method == "getInformation")))
                .ReturnsAsync("Arrakis");
            rpcClient.Setup(c => c.ExecuteTaskAsync<IEnumerable<SystemInformation>> (It.IsAny<IRpcRequest>()))
                .ReturnsAsync(slt);



            //rpcClient.Setup(c => c.ExecuteTaskAsync<object>(It.Is<IRpcRequest>(t => t.Method == "getInformation")))
            //    .ReturnsAsync("Arrakis");
                
            var obj = new OmvSystemClient(rpcClient.Object);
            Assert.IsNotNull(obj);
            obj.SetOMVVersion(OMVVersions.Version4);
            var result = await obj.GetSystemInformations();
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task GetSystemInformationsV5()
        {
            var rpcClient = new Mock<IRpcClient>();

            //rpcClient.Setup(c => c.ExecuteTaskAsync<object>(It.Is<IRpcRequest>(t => t.Method == "getInformation")))
            //    .ReturnsAsync("Usul");

            //rpcClient.SetupSequence(c => c.ExecuteTaskAsync<object>(It.IsAny<IRpcRequest>()))
            //    .ReturnsAsync("Usul")
            //    .ReturnsAsync(new OMVInformations());


            //    rpcClient.Setup(c => c.ExecuteTaskAsync<It.IsSubtype<OMVInformations>(It.Is<IRpcRequest>(t => t.Method == "getInformation")))
            //    .ReturnsAsync("Usul");

            rpcClient.Setup(c => c.ExecuteTaskAsync<object>(It.Is<IRpcRequest>(t => t.Method == "getInformation")))
                .ReturnsAsync("Usul");
            rpcClient.Setup(c => c.ExecuteTaskAsync<OMVInformations>(It.Is<IRpcRequest>(t => t.Method == "getInformation")))
                .ReturnsAsync(new OMVInformations());
            var obj = new OmvSystemClient(rpcClient.Object);
            Assert.IsNotNull(obj);
            obj.SetOMVVersion(OMVVersions.Version5);
            var result = await obj.GetSystemInformations();
            Assert.IsNotNull(result);
        }


        //case "CPU usage":
        //    obj.CpuUsage = information.Value.ValueClass.Value;
        //    break;
        //case "":
        //    obj.MemTotal = long.Parse(information.Value.SimpleStringValue);
        //    break;
        //case "Memory usage":
        //    obj.MemUsed = long.Parse(information.Value.ValueClass.Value.ToString());
        //    break;




    }
}
