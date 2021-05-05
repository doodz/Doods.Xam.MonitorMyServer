using Doods.Framework.Std.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Validation
{
    [TestClass]
    public class IsNumericRuleUnitTest
    {

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Create(bool allowdecimal)
        {
            var obj = new IsNumericRule<string>(allowdecimal);

            Assert.IsNotNull(obj);

            Assert.IsNull(obj.ValidationMessage);
        }

        [DataTestMethod]
        [DataRow("1", true)]
        [DataRow("1", false)]
        //[DataRow("1,1", true)]
        //[DataRow("1,1", false)]
        //[DataRow("1.000,1", true)]
        //[DataRow("1.000,1", false)]
        public void CheckValid(string str, bool allowdecimal)
        {
            var obj = new IsNumericRule<string>(allowdecimal);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Check(str));

        }
        [DataTestMethod]
        [DataRow("",true)]
        [DataRow("", false)]
        [DataRow(" ",true)]
        [DataRow(" ", false)]
        //[DataRow(null,true)]
        //[DataRow(null, false)]
        public void CheckInValid(string str ,bool allowdecimal)
        {
            var obj = new IsNumericRule<string>(allowdecimal);
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.Check(str));

        }

        [DataTestMethod]
        [DataRow(1,true)]
        [DataRow(1,false)]
        [DataRow(1.1F, true)]
        [DataRow(1F, false)]
        [DataRow(1.1F, true)]
        [DataRow(1F, false)]
        //[DataRow(new bool(),true)]
        //[DataRow(new bool(),false)]
        public void CheckInValidTypeobject(object type,bool allowdecimal)
        {
            var obj = new IsNumericRule<object>(allowdecimal);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Check(type));

        }
    }
}