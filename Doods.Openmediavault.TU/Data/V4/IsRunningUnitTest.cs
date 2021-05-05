using Doods.Openmediavault.Rpc.std.Data.V4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.TU.Data.V5
{
    [TestClass]
    public class IsRunningUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new IsRunning();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.Running);

            Assert.IsNull(obj.Filename);
        }
    }
}