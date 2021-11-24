using Doods.Openmediavault.Rpc.Std.Data.V4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.Rpc.Std.TU
{
    [TestClass]
    public class RequestbuilderUnitTest
    {
        [TestMethod]
        public void Create_default()
        {
            var obj = new Requestbuilder();
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void ToJson()
        {
            var error = new OMVError();
            var obj = Requestbuilder.ToJson(error);
            Assert.IsNotNull(obj);
            Assert.AreNotEqual(string.Empty,obj);
        }
    }
}