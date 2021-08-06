using Doods.Synology.Webapi.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Synology.TU
{
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