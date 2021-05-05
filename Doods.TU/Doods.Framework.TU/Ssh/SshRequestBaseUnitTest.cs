using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Requests;
using Doods.Framework.Ssh.Std.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Ssh
{
    [TestClass]
    public class SshRequestBaseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var cmd = "echo toto";
            var obj = new SshRequestBase(cmd);
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.NeedSudo);
            Assert.AreEqual(cmd,obj.CommandText);
            Assert.IsNotNull(obj.Handler);
            Assert.IsNotNull(obj.NeedGroup);
            Assert.AreEqual(0, obj.NeedGroup.Count());
        }
        [TestMethod]
        public void CreateMock()
        {

            var mockSettings = new Mock<ISshSerializerSettings>();
            mockSettings.Setup(m => m.Converters).Returns(() => new List<ISshConverter>());
           
          
            var cmd = "echo toto";
            var obj = new SshRequestBase(cmd, mockSettings.Object);
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.NeedSudo);
            Assert.AreEqual(cmd, obj.CommandText);
            Assert.IsNotNull(obj.Handler);
            Assert.IsNotNull(obj.NeedGroup);
            Assert.AreEqual(0, obj.NeedGroup.Count());
        }
    }
}