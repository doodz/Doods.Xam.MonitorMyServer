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
}
