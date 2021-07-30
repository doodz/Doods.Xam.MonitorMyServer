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
            //Assert.AreEqual(nameof(LoadingContext.RefreshVisual),obj.);

        }

        //public bool ReinitializeLists { get; }

        //public bool NotifyUser { get; }

        //public CancellationToken Token { get; }

        //public ITimeWatcher Timer { get; }

        //public bool IsValid { get; }
    }
}
