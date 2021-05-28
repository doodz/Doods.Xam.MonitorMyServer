using Doods.Openmediavault.Rpc.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.TU.Rpc
{
    [TestClass]
    public class DefaultSshRequestUnitTest
    {

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("long.MaxValue")]
        [DataRow("")]
        [DataRow("foo")]
        public void Create(string requestString)
        {
            var obj = new DefaultSshRequest(requestString);
            Assert.IsNotNull(obj);
            Assert.AreEqual(requestString,obj.CommandText);
        }
    }
}