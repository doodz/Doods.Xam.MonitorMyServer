using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Mvvm
{
    [TestClass]
    public class LoadingContextUnitTest
    {

        [TestMethod]
        public void CreateFromUser()
        {
            var timeWatcher = new Mock<ITimeWatcher>();
            var obj = LoadingContext.FromUser;

            Assert.IsNotNull(obj);
            Assert.AreEqual(CancellationToken.None, obj.Token);
            Assert.IsTrue(obj.ReinitializeLists);
            Assert.IsTrue(obj.NotifyUser);
            Assert.IsFalse(obj.IsValid);
            Assert.IsNull(obj.Timer);


        }

        [TestMethod]
        public void CreateOnAppearing()
        {
            var timeWatcher = new Mock<ITimeWatcher>();
            var obj = LoadingContext.OnAppearing;

            Assert.IsNotNull(obj);
            Assert.AreEqual(CancellationToken.None, obj.Token);
            Assert.IsTrue(obj.ReinitializeLists);
            Assert.IsTrue(obj.NotifyUser);
            Assert.IsFalse(obj.IsValid);
            Assert.IsNull(obj.Timer);


        }

        [TestMethod]
        public void CreateRefreshVisual()
        {
            var timeWatcher = new Mock<ITimeWatcher>();
            var obj = LoadingContext.RefreshVisual;

            Assert.IsNotNull(obj);
            Assert.AreEqual(CancellationToken.None, obj.Token);
            Assert.IsFalse(obj.ReinitializeLists);
            Assert.IsFalse(obj.NotifyUser);
            Assert.IsFalse(obj.IsValid);
            Assert.IsNull(obj.Timer);

        }

        //[TestMethod]
        //public void My_GetHashCode()
        //{
        //    var timeWatcher = new Mock<ITimeWatcher>();
        //    var obj1 = LoadingContext.OnAppearing;
        //    var obj2 = LoadingContext.RefreshVisual;
        //    var obj3 = LoadingContext.FromUser;
        //    Assert.AreEqual(823854720, obj1.GetHashCode());
        //    Assert.AreEqual(2, obj2.GetHashCode());
        //    Assert.AreEqual(3, obj3.GetHashCode());
        //}


        [TestMethod]
        public void My_Equals_false()
        {
            var timeWatcher = new Mock<ITimeWatcher>();
            var obj1 = LoadingContext.OnAppearing;
            var obj2 = LoadingContext.RefreshVisual;
            Assert.AreNotEqual(obj1, obj2);
        }

        [TestMethod]
        public void My_Equals_true()
        {
            var timeWatcher = new Mock<ITimeWatcher>();
            var obj1 = LoadingContext.OnAppearing;
            var obj2 = LoadingContext.OnAppearing;
            Assert.AreEqual(obj1, obj2);

        }
      
    }
}
