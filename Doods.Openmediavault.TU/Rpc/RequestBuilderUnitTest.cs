using System;
using System.Collections.Generic;
using System.Text;
using Doods.Openmediavault.Rpc.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.TU.Rpc
{
    [TestClass]
    public class RequestBuilderUnitTest
    {

        [TestMethod]
        public void Create()
        {
            var obj = new RpcRequest();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Method);
            Assert.IsNull(obj.Options);
            Assert.IsNull(obj.Service);
            Assert.IsNull(obj.Params);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("long.MaxValue")]
        [DataRow("")]
        [DataRow("foo")]
        public void Set_Method(string method)
        {

            var obj = new RpcRequest();
            Assert.IsNotNull(obj);
            obj.Method = method;

            Assert.AreEqual(method,obj.Method);
            Assert.IsNull(obj.Options);
            Assert.IsNull(obj.Service);
            Assert.IsNull(obj.Params);

        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("long.MaxValue")]
        [DataRow("")]
        [DataRow("foo")]
        public void Set_Service(string service)
        {

            var obj = new RpcRequest();
            Assert.IsNotNull(obj);
            obj.Service = service;

            Assert.IsNull(obj.Method);
            Assert.IsNull(obj.Options);
            Assert.AreEqual(service, obj.Service);
            Assert.IsNull(obj.Params);

        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("long.MaxValue")]
        [DataRow("")]
        [DataRow("foo")]
        public void Set_Params(object localParams)
        {

            var obj = new RpcRequest();
            Assert.IsNotNull(obj);
            obj.Params = localParams;

            Assert.IsNull(obj.Service);
            Assert.IsNull(obj.Options);
            Assert.IsNull(obj.Service);
            Assert.AreEqual(localParams, obj.Params);
          

        }
    }
}
