using Doods.Synology.Webapi.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Doods.Synology.TU
{
    [TestClass]
    public class SynologyRestRequestUnitTest
    {
        [DataTestMethod]
        [DataRow("long.MaxValue")]
        [DataRow("")]
        [DataRow("foo")]
        public void Create(string resource)
        {
            var obj = new SynologyRestRequest(resource);
            Assert.IsNotNull(obj);
            Assert.AreEqual(resource, obj.Resource);
            Assert.AreEqual(Method.GET, obj.Method);
            Assert.AreEqual(DataFormat.Xml, obj.RequestFormat);


        }
    }

    [TestClass]
    public class SynologyResponseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new SynologyResponse<string>();
            Assert.IsNotNull(obj);
            Assert.AreEqual(default(int), obj.Code);
            Assert.IsNull(obj.Data);
            Assert.IsNull(obj.Error);
            Assert.IsNull(obj.Errormsg);
            Assert.IsNull(obj.Errors);
            Assert.AreEqual(default(int), obj.HttpStatus);
            Assert.IsFalse(obj.Success);

        }

        [DataTestMethod]
        [DataRow("long.MaxValue")]
        [DataRow("")]
        [DataRow("foo")]
        [DataRow(null)]
        public void Set_Data(string data)
        {
            var obj = new SynologyResponse<string>();
            Assert.IsNotNull(obj);
            obj.Data = data;

            Assert.AreEqual(data, obj.Data);
            Assert.AreEqual(default(int), obj.Code);
            Assert.IsNull(obj.Error);
            Assert.IsNull(obj.Errormsg);
            Assert.IsNull(obj.Errors);
            Assert.AreEqual(default(int), obj.HttpStatus);
            Assert.IsFalse(obj.Success);


        }
    }
}
