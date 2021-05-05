using Doods.Framework.Std.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Validation
{
    [TestClass]
    public class IsGoodPasswordUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new IsGoodPassword<string>();

            Assert.IsNotNull(obj);

            Assert.IsNull(obj.ValidationMessage);
        }

        [DataTestMethod]
        [DataRow("azertyuiop")]// :-/
        [DataRow("&é\"'(-è_çà)='")]
        [DataRow("123456789")]// :-/
        public void CheckValid(string password)
        {
            var obj = new IsGoodPassword<string>();
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Check(password));

        }
        [DataTestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("_")]
        [DataRow("&é\"'(-'")]
        [DataRow("azerty")]
        [DataRow(null)]
        public void CheckInValid(string password)
        {
            var obj = new IsGoodPassword<string>();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.Check(password));

        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(new bool())]
        public void CheckInValidTypeString(object type)
        {
            var obj = new IsGoodPassword<object>();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.Check(type));

        }
    }
}