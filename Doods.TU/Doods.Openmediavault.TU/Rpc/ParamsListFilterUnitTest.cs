using Doods.Openmediavault.Rpc.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Openmediavault.TU.Rpc
{
    [TestClass]
    public class ParamsListFilterUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new ParamsListFilter();
            Assert.IsNotNull(obj);
            Assert.AreEqual(default(int),obj.Start);
            Assert.AreEqual(25,obj.Limit);
          
        }

        [DataTestMethod]
        [DataRow(int.MaxValue,int.MaxValue)]
        [DataRow(int.MinValue, int.MinValue)]
        [DataRow(default(int), default(int))]
        [DataRow(int.MaxValue, int.MinValue)]
        [DataRow(12, 24)]
        public void Create(int start,int limit)
        {
            var obj = new ParamsListFilter(start, limit);
            Assert.IsNotNull(obj);
            Assert.AreEqual(start, obj.Start);
            Assert.AreEqual(limit, obj.Limit);

        }
        [DataTestMethod]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        [DataRow(default(int))]
        [DataRow(int.MaxValue)]
        [DataRow(12)]
        public void Set_Start(int start)
        {
            var obj = new ParamsListFilter();
            Assert.IsNotNull(obj);
            obj.Start = start;
            Assert.AreEqual(start, obj.Start);

        }
        [DataTestMethod]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        [DataRow(default(int))]
        [DataRow(int.MaxValue)]
        [DataRow(12)]
        public void Set_Limit( int limit)
        {
            var obj = new ParamsListFilter();
            Assert.IsNotNull(obj);
            obj.Limit = limit;
            Assert.AreEqual(limit, obj.Limit);

        }
    }
}