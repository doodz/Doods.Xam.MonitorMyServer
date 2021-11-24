using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doods.Openmediavault.Rpc.Std.Seruializer;
namespace Doods.Openmediavault.Rpc.Std.TU.Seruializer
{
    [TestClass]
    public class ParseEscapeStringConverterUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new ParseEscapeStringConverter();
            Assert.IsNotNull(obj);

        }

        [TestMethod]
        public void CanConvert()
        {
            var obj = new ParseEscapeStringConverter();
            Assert.IsNotNull(obj);
            var b = obj.CanConvert(this.GetType());

        }
    }
}
