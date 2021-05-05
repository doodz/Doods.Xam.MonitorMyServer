using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Std.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Validation
{
    [TestClass]
    public class EmailRuleUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new EmailRule<string>();

            Assert.IsNotNull(obj);

            Assert.IsNull(obj.ValidationMessage);
        }

        [TestMethod]
        public void Create_SetGetValidationMessage()
        {
            var msg = "toto tata tutu";
            var obj = new EmailRule<string>()
            {
                ValidationMessage = msg
            };

            Assert.IsNotNull(obj);

            Assert.AreEqual(msg,obj.ValidationMessage);
        }

        [DataTestMethod]
        [DataRow("toto@tutu.fr")]
        [DataRow("toto@tutu.com")]
        [DataRow("toto.tutu@tata.fr")]
        public void CheckValid(string email)
        {
            var obj = new EmailRule<string>();
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Check(email));

        }
        [DataTestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("_")]
        [DataRow("titi.fr")]
        [DataRow("@tutu.fr")]
        [DataRow("toto@tutu.fr_fr")]
        public void CheckInValid(string email)
        {
            var obj = new EmailRule<string>();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.Check(email));

        }
        [TestMethod]
        public void CheckNull()
        {
            var obj = new EmailRule<string>();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.Check(null));

        }
    }
}
