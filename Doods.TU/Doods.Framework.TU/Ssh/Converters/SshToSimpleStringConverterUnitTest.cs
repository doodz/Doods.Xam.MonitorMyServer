using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Documents;
using Doods.Framework.Ssh.Std.Converters;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Ssh.Converters
{
    [TestClass]
    public class SshToSimpleStringConverterUnitTest
    {
        [TestMethod]
        public void Create()
        {
          
            var obj = new SshToSimpleStringConverter();
            Assert.IsNotNull(obj);

        }

        [DataTestMethod]
        [DataRow(typeof(IEnumerable<string>))]
        [DataRow(typeof(ICollection<string>))]
        [DataRow(typeof(IList<string>))]
        [DataRow(typeof(List<string>))]
        [DataRow(typeof(Collection<string>))]
        [DataRow(typeof(string[]))]
        public void CanConvert_True(Type t)
        {

            var obj = new SshToSimpleStringConverter();
            Assert.IsNotNull(obj);
            var result = obj.CanConvert(t);
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow(typeof(string))]
        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataRow(typeof(Array))]
        [DataRow(typeof(IEnumerable<int>))]
        [DataRow(typeof(ICollection<object>))]
        [DataRow(typeof(IList<bool>))]
        [DataRow(typeof(List))]
        [DataRow(typeof(Collection))]
        [DataRow(typeof(char[]))]
        public void CanConvert_False(Type t)
        {

            var obj = new SshToSimpleStringConverter();
            Assert.IsNotNull(obj);
            var result = obj.CanConvert(t);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("",typeof(IEnumerable<string>))]
        [DataRow("",typeof(ICollection<string>))]
        [DataRow("",typeof(IList<string>))]
        [DataRow("",typeof(List<string>))]
        [DataRow("",typeof(Collection<string>))]
        [DataRow("", typeof(string[]))]
        public void Read(string content,Type t)
        {

            var obj = new SshToSimpleStringConverter();
            Assert.IsNotNull(obj);
            var result = obj.Read(content,t);
            
            

            Assert.IsInstanceOfType(result,t);
        }
    }
}
