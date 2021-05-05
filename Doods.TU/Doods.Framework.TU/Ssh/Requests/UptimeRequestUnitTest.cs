using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Ssh.Std.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Ssh.Requests
{
   
    [TestClass]
    public class UptimeRequestUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new UptimeRequest();
            Assert.IsNotNull(obj);
            Assert.AreEqual(UptimeRequest.RequestString,obj.CommandText);
            
        }
    }
}
