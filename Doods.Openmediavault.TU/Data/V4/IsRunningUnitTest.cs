using System;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.TU.Data.V4
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