using System;
using System.Collections;
using Doods.Framework.Std.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Validation
{
    [TestClass]
    public class IsBadFormetedUrlRuleUnitTest
    {
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Create(bool needhttp)
        {
            var obj = new IsBadFormetedUrlRule<string>(needhttp);

            Assert.IsNotNull(obj);
            Assert.IsNull(obj.ValidationMessage);
        }

        [TestMethod]
        public void Create_SetGetValidationMessage()
        {
            var msg = "toto tata tutu";
            var obj = new IsBadFormetedUrlRule<string>(false)
            {
                ValidationMessage = msg
            };

            Assert.IsNotNull(obj);

            Assert.AreEqual(msg, obj.ValidationMessage);
        }

        [DataTestMethod]
        [DataRow("http://google.Fr", true)]
        [DataRow("https://google.Fr", true)]
        [DataRow("google.Fr", false)]
        [DataRow("google.Fr", false)]
        [DataRow("http://google.Fr/", true)]
        [DataRow("https://google.Fr/", true)]
        [DataRow("//google.Fr/", false)]
        [DataRow("//google.Fr/", false)]
        public void CheckValid(string url,bool needhttp)
        {
            var obj = new IsBadFormetedUrlRule<string>(needhttp);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Check(url));

        }
        [DataTestMethod]
        [DataRow("", true)]
        [DataRow("", false)]
        [DataRow(null, true)]
        [DataRow(null, false)]
        [DataRow("http://google.Fr", false)]
        [DataRow("https://google.Fr", false)]
        [DataRow("google.Fr", true)]
        [DataRow("http:/google.Fr", true)]
        [DataRow("https:/google.Fr", true)]
        // [DataRow("/google.Fr", false)]
        [DataRow("/google.Fr", true)]
        public void CheckInValid(string url, bool needhttp)
        {
            var obj = new IsBadFormetedUrlRule<string>(needhttp);
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.Check(url));

        }
    }
}