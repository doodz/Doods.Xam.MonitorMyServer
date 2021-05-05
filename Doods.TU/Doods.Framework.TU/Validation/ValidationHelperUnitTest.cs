using Doods.Framework.Std.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Validation
{
    [TestClass]
    public class ValidationHelperUnitTest
    {

        [DataTestMethod]

        public void Create()
        {
            var obj = new ValidationHelper();

            Assert.IsNotNull(obj);
        }

        [DataTestMethod]
        [DataRow("")]
       
        public void ISBlanckString_true(string str)
        {
            var obj = new ValidationHelper();

            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.IsBlankString(str));


        }

        [DataTestMethod]
        [DataRow(" ")]
        [DataRow("\t")]
        [DataRow("\r")]
        [DataRow("\n")]
        [DataRow("azertyuiop")]
        [DataRow("azertyuiop ")]
        [DataRow("azertyuiop\t")]
        [DataRow("azertyuiop\r")]
        [DataRow("azertyuiop\n")]
        public void ISBlanckString_false(string str)
        {
            var obj = new ValidationHelper();

            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.IsBlankString(str));

        }
    }
}