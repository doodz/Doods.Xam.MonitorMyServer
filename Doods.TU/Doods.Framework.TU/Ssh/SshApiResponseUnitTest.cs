using System;
using System.Text;
using Doods.Framework.Ssh.Std;
using Doods.Framework.Ssh.Std.Enums;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Ssh
{
    [TestClass]
    public class SshApiResponseUnitTest
    {

        [TestMethod]
        public void Create()
        {
            var obj = new SshApiResponse();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Content);
            Assert.AreEqual(0L,obj.ContentLength);
            Assert.IsNull(obj.ErrorException);
            Assert.IsNull(obj.ErrorMessage);
            Assert.AreEqual(0,obj.ExitStatus);
            Assert.AreEqual(ResponseStatus.None,obj.ResponseStatus);
            Assert.IsNull(obj.Request);
        }


        [TestMethod]
        public void SetValues()
        {
            var content = "toto";
            var errorMsg = "My errors _";
            var statExit = 42;
            var StatusCode = 12;
            var obj = new SshApiResponse()
            {
                Content = content,
                ContentLength = content.Length,
                ErrorMessage = errorMsg,
                ExitStatus = statExit,
                StatusCode = StatusCode,
            };
            Assert.IsNotNull(obj);
            Assert.AreEqual(content, obj.Content);
            Assert.AreEqual(content.Length, obj.ContentLength);
            Assert.AreEqual(errorMsg, obj.ErrorMessage);
            Assert.AreEqual(statExit, obj.ExitStatus);
            Assert.AreEqual(StatusCode, obj.StatusCode);
        }
    }
}
