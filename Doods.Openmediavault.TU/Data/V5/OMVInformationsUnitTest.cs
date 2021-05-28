using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Requests;
using Doods.Framework.Ssh.Std.Serializers;
using Doods.Openmediavault.Rpc.Std.Data.V5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NuGet.Frameworks;

namespace Doods.Openmediavault.TU.Data.V5
{
    [TestClass]
    public class OMVInformationsUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new OMVInformations();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.LegacyMode);
            Assert.AreEqual(default(long),obj.Ts);
            Assert.IsNull(obj.Time);
            Assert.IsNull(obj.Hostname);
            Assert.IsNull(obj.Version);
            Assert.IsNull(obj.CpuModelName);
            Assert.AreEqual(default(double), obj.CpuUsage);
            Assert.AreEqual(default(long), obj.MemTotal);
            Assert.AreEqual(default(long), obj.MemUsed);
            Assert.IsNull(obj.Kernel);
            Assert.IsNull(obj.Uptime);
            Assert.IsNull(obj.LoadAverage);
            Assert.IsFalse(obj.ConfigDirty);
            Assert.IsFalse(obj.RebootRequired);
            Assert.IsFalse(obj.PkgUpdatesAvailable);
        }
    }
}
