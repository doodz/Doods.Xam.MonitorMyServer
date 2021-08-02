using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Converters;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU.Ssh.Converters
{
    [TestClass]
    public class SshToProcessConverterUnitTest
    {
        [TestMethod]
        public void Create()
        {

            var obj = new SshToProcessConverter();
            Assert.IsNotNull(obj);

        }
        [TestMethod]
        [DataRow(typeof(IEnumerable<ProcessBean>))]
        //[DataRow(typeof(ICollection<ProcessBean>))]
        //[DataRow(typeof(IList<ProcessBean>))]
        //[DataRow(typeof(List<ProcessBean>))]
        //[DataRow(typeof(Collection<ProcessBean>))]
        //[DataRow(typeof(ProcessBean[]))]
        public void CanConvert_True(Type t)
        {

            var obj = new SshToProcessConverter();
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
        [DataRow(typeof(List<object>))]
        [DataRow(typeof(Collection<object>))]
        [DataRow(typeof(char[]))]
     
        public void CanConvert_False(Type t)
        {

            var obj = new SshToProcessConverter();
            Assert.IsNotNull(obj);
            var result = obj.CanConvert(t);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("", typeof(IEnumerable<ProcessBean>))]
        //[DataRow("", typeof(ICollection<ProcessBean>))]
        //[DataRow("", typeof(IList<ProcessBean>))]
        //[DataRow("", typeof(List<ProcessBean>))]
        //[DataRow("", typeof(Collection<ProcessBean>))]
        //[DataRow("", typeof(ProcessBean[]))]
        public void Read(string content, Type t)
        {

            var obj = new SshToProcessConverter();
            Assert.IsNotNull(obj);
            var result = obj.Read(content, t);



            Assert.IsInstanceOfType(result, t);
        }
    }
}