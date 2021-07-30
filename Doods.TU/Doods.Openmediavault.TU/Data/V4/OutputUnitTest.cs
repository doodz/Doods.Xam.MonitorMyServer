using System;
using System.Collections.Generic;
using System.Text;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.TU.Data.V4
{
    [TestClass]
    public class OutputUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new Output<string>();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Filename);

            Assert.AreEqual(default(int), obj.Pos);
            Assert.AreEqual(false, obj.Running);
            Assert.AreEqual(null, obj.Content);

        }


        [TestMethod]
        public void Create_Type_string()
        {
            var obj = new Output<string>();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Filename);

            Assert.AreEqual(default(int), obj.Pos);
            Assert.AreEqual(false, obj.Running);
            Assert.AreEqual(null, obj.Content);
            //Assert.AreEqual(typeof(string),obj.Content?.GetType());


        }

        [DataTestMethod]
        [DataRow(typeof(string))]
        [DataRow(typeof(object))]
        public void Create_Type(Type type)
        {
            var obj = new Output<string>();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Filename);

            Assert.AreEqual(default(int), obj.Pos);
            Assert.AreEqual(false, obj.Running);
            Assert.AreEqual(null, obj.Content);
            //Assert.AreEqual(type, obj.Content.GetType());
        }
    }
    [TestClass]
    public class OutputTUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new Output();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Filename);

            Assert.AreEqual(default(int), obj.Pos);
            Assert.AreEqual(false, obj.Running);
            Assert.AreEqual(null, obj.Content);
        }
        [DataTestMethod]
        [DataRow(" ")]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("foo")]
        public void Set_Content(string content)
        {
            var obj = new Output();
            Assert.IsNotNull(obj);
            obj.Content = content;
            Assert.AreEqual(content, obj.Content);
        }

    }
    [TestClass]
    public class OutputBaseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new OutputBase();
            Assert.IsNotNull(obj);
            Assert.IsNull(obj.Filename);

            Assert.AreEqual(default(int), obj.Pos);
            Assert.AreEqual(false, obj.Running);
        }

        [DataTestMethod]
        [DataRow(" ")]
        [DataRow(null)]
        [DataRow("foo")]
        public void Set_Filename(string name)
        {
            var obj = new OutputBase();
            Assert.IsNotNull(obj);
            obj.Filename = name;
            Assert.AreEqual(name, obj.Filename);
        }
        [DataTestMethod]
        [DataRow(default(int))]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        [DataRow(42)]
        public void Set_Pos(int pos)
        {
            var obj = new OutputBase();
            Assert.IsNotNull(obj);
            obj.Pos = pos;
            Assert.AreEqual(pos, obj.Pos);
        }
        [DataTestMethod]
        [DataRow(default(bool))]
        [DataRow(true)]
        public void Set_Running(bool running)
        {
            var obj = new OutputBase();
            Assert.IsNotNull(obj);
            obj.Running = running;
            Assert.AreEqual(running, obj.Running);
        }
    }
}
