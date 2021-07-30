using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Openmediavault.TU.Data.V4
{
    [TestClass]
    public class ResponseUnitTest
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
    }

    [TestClass]
    public class ResponseArrayUnitTest
    {
        [TestMethod]
        public void Create()
        {

            //var OmvObjectMock = new Mock<IOmvObject>();
            var obj = new ResponseArray<OmvObject>();
            Assert.IsNotNull(obj);
            Assert.AreEqual(default(long), obj.Total);
            Assert.IsNull(obj.Data);

        }

        [DataTestMethod]
        [DataRow(default(int))]
        [DataRow(long.MaxValue)]
        [DataRow(long.MinValue)]
        [DataRow(42)]
        public void Set_Total(long total)
        {

            //var OmvObjectMock = new Mock<IOmvObject>();
            var obj = new ResponseArray<OmvObject>();
            Assert.IsNotNull(obj);
            obj.Total = total;
            Assert.AreEqual(total, obj.Total);

        }

        [TestMethod]
        public void Set_Data()
        {

            var OmvObjectMock = new Mock<IOmvObject>();
            var obj = new ResponseArray<IOmvObject>();
            Assert.IsNotNull(obj);
            obj.Data = new []{ OmvObjectMock.Object };
            Assert.AreEqual(1, obj.Data.Length);
            Assert.AreEqual(OmvObjectMock.Object, obj.Data[0]);
        }
    }

    [TestClass]
    public class ResponseListUnitTest
    {
        [TestMethod]
        public void Create()
        {

            //var OmvObjectMock = new Mock<IOmvObject>();
            var obj = new ResponseList<OmvObject>();
            Assert.IsNotNull(obj);
            Assert.AreEqual(default(long), obj.Total);
            Assert.IsNull(obj.Data);

        }

        [DataTestMethod]
        [DataRow(default(int))]
        [DataRow(long.MaxValue)]
        [DataRow(long.MinValue)]
        [DataRow(42)]
        public void Set_Total(long total)
        {

            //var OmvObjectMock = new Mock<IOmvObject>();
            var obj = new ResponseList<OmvObject>();
            Assert.IsNotNull(obj);
            obj.Total = total;
            Assert.AreEqual(total, obj.Total);

        }

        [TestMethod]
        public void Set_Data()
        {

            var OmvObjectMock = new Mock<IOmvObject>();
            var obj = new ResponseList<IOmvObject>();
            Assert.IsNotNull(obj);
            obj.Data = new List<IOmvObject>();
            Assert.AreEqual(0, obj.Data.Count);
            //Assert.AreEqual(OmvObjectMock, obj.Data[1]);
        }


        [TestMethod]
        public void Set_Data1()
        {

            var OmvObjectMock = new Mock<IOmvObject>();
            var obj = new ResponseList<IOmvObject>();
            Assert.IsNotNull(obj);
            obj.Data = new List<IOmvObject>(){ OmvObjectMock.Object};
            Assert.AreEqual(1, obj.Data.Count);
            Assert.AreEqual(OmvObjectMock.Object, obj.Data[0]);
        }
    }
}
