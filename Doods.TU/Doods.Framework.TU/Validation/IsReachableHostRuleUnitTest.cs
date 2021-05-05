using System;
using Doods.Framework.Std.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Validation
{
    [TestClass]
    public class IsReachableHostRuleUnitTest
    {

        [DataTestMethod]

        public void Create()
        {
            var obj = new IsReachableHostRule<string>();

            Assert.IsNotNull(obj);

            Assert.IsNull(obj.ValidationMessage);
        }
    }
}