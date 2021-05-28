using Doods.Synology.Webapi.Std.NewFolder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Synology.TU.Datas
{
    [TestClass]
    public class SynoLoginInfoUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var v = new SynoLoginInfo();
            Assert.IsNotNull(v);
            Assert.IsNull(v.Sid);
        }

        [TestMethod]
        public void Set_Sid()
        {
            var id = "";
            var v = new SynoLoginInfo();
            Assert.IsNotNull(v);
            v.Sid = id;
            Assert.AreEqual(id,v.Sid);
        }
    }
}
