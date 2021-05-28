using Doods.Openmediavault.Rpc.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Doods.Openmediavault.TU.Rpc
{
    [TestClass]
    public class LocalRestRequestUnitTest
    {

        [DataTestMethod]
        [DataRow("long.MaxValue")]
        [DataRow("")]
        [DataRow("foo")]
        public void Create(string resource)
        {
            var obj = new LocalRestRequest(resource,Method.GET);
            Assert.IsNotNull(obj);
            Assert.AreEqual(resource,obj.Resource);
            Assert.AreEqual(Method.GET, obj.Method);
            Assert.AreEqual(DataFormat.Json, obj.RequestFormat);
            

        }
    }
}