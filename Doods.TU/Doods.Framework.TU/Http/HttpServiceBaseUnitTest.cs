using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Http.Std;
using Doods.Framework.Ssh.Std.Serializers;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Http
{
    [TestClass]
    public class HttpRequestUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var rsc = "toto";
            var mth = RestSharp.Method.GET;
            var obj = new HttpRequest(rsc, mth);
            Assert.IsNotNull(obj);
            Assert.AreEqual(rsc, obj.Resource);
            Assert.AreEqual(mth, obj.Method);
            Assert.IsInstanceOfType(obj.JsonSerializer, typeof(Doods.Framework.Http.Std.Serializers.NewtonsoftJsonSerializer));
        }
    }


    [TestClass]
    public class HttpServiceBaseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var mockLogger = new Mock<ILogger>();

            var obj = new HttpServiceBase(mockLogger.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Logger);
            Assert.AreEqual(mockLogger.Object, obj.Logger);
        }
    }
}
