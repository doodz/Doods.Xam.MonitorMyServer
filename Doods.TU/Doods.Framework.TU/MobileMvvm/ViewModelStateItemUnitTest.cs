using System;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.MobileMvvm
{
    [TestClass]
    public class ViewModelStateItemUnitTest
    {
        [TestMethod]
        public void Create()
        {
            //var loggertMock = new Mock<ILogger>();
            //var telemetryServiceMock = new Mock<ITelemetryService>();
            var viewModelMock = new Mock<IViewModel>();
            var MainThread = new Mock<Xamarin.Essentials.Interfaces.IMainThread>();
            var obj = new ViewModelStateItem(viewModelMock.Object, MainThread.Object);
            Assert.IsNotNull(obj);
           
            Assert.AreEqual(viewModelMock.Object,obj.ViewModel);
            Assert.AreEqual(SvgIconTarget.Info, obj.Icon);
            Assert.AreEqual(obj.ViewModel.CmdState, obj.ShowCurrentCmd);
           
            Assert.IsNull(obj.Color);
            Assert.IsNull(obj.Title);
            Assert.IsNull(obj.Subtitle);
            Assert.IsNull(obj.Description);

            Assert.IsFalse(obj.IsRunning);
        }

        [TestMethod]
        public void RunAction()
        {
            var viewModelMock = new Mock<IViewModel>();
            var MainThread = new Mock<Xamarin.Essentials.Interfaces.IMainThread>();
            var obj = new ViewModelStateItem(viewModelMock.Object, MainThread.Object);
            Assert.IsNotNull(obj);
            Action act = () =>
            {
                Assert.IsTrue(obj.IsRunning);
            };
            obj.RunAction(act);
            Assert.IsFalse(obj.IsRunning);
        }
        //[TestMethod]
        //public async Task RunActionAsync()
        //{
        //    var viewModelMock = new Mock<IViewModel>();
        //    var obj = new ViewModelStateItem(viewModelMock.Object);
        //    Assert.IsNotNull(obj);
        //    bool myActionCalled = false;
        //    bool BeforeCalled = false;
        //    bool AfterCalled = false;
        //    Func<Task> myAction = () =>
        //    {
        //        myActionCalled = true;
        //        Assert.IsTrue(obj.IsRunning);
        //        return Task.FromResult(0);
        //    };
        //    Action before = () =>
        //    {
        //        BeforeCalled = true;
        //        Assert.IsFalse(obj.IsRunning);
        //    };
        //    Action after = () =>
        //    {
        //        AfterCalled = true;
        //        Assert.IsFalse(obj.IsRunning);
        //    };
        //    await obj.RunActionAsync(myAction, before, after);
        //    Assert.IsFalse(obj.IsRunning);
        //    Assert.IsTrue(myActionCalled);
        //    Assert.IsTrue(BeforeCalled);
        //    Assert.IsTrue(AfterCalled);
        //}
        [TestMethod]
        public void RunActions()
        {
            var viewModelMock = new Mock<IViewModel>();
            var MainThread = new Mock<Xamarin.Essentials.Interfaces.IMainThread>();
            var obj = new ViewModelStateItem(viewModelMock.Object, MainThread.Object);
            Assert.IsNotNull(obj);
            bool myActionCalled = false;
            bool BeforeCalled = false;
            bool AfterCalled = false;
            Action myAction = () =>
            {
                myActionCalled = true;
                Assert.IsTrue(obj.IsRunning);
                
            };
            Action before = () =>
            {
                BeforeCalled = true;
                Assert.IsFalse(obj.IsRunning);
            };
            Action after = () =>
            {
                AfterCalled = true;
                Assert.IsFalse(obj.IsRunning);
            };
            obj.RunActions(myAction, before, after);
            Assert.IsFalse(obj.IsRunning);
            Assert.IsTrue(myActionCalled);
            Assert.IsTrue(BeforeCalled);
            Assert.IsTrue(AfterCalled);
        }
        [TestMethod]
        public void RunFunc1()
        {
            var viewModelMock = new Mock<IViewModel>();
            var MainThread = new Mock<Xamarin.Essentials.Interfaces.IMainThread>();
            var obj = new ViewModelStateItem(viewModelMock.Object, MainThread.Object);
            Assert.IsNotNull(obj);
            bool myActionCalled = false;
          
            Func<string> myAction = () =>
            {
                myActionCalled = true;
                Assert.IsTrue(obj.IsRunning);
                return "ok_1";
            };
          
            var result =obj.RunFunc<string>(myAction);
            Assert.IsFalse(obj.IsRunning);
            Assert.IsTrue(myActionCalled);
            Assert.AreEqual("ok_1", result);
        }
        [TestMethod]
        public void RunFunc2()
        {
            var viewModelMock = new Mock<IViewModel>();
            var MainThread = new Mock<Xamarin.Essentials.Interfaces.IMainThread>();
            var obj = new ViewModelStateItem(viewModelMock.Object, MainThread.Object);
            Assert.IsNotNull(obj);
            bool myActionCalled = false;
            bool BeforeCalled = false;
            bool AfterCalled = false;
            Func<string> myAction = () =>
            {
                myActionCalled = true;
                Assert.IsTrue(obj.IsRunning);
                return "ok_2";
            };
            Action before = () =>
            {
                BeforeCalled = true;
                Assert.IsFalse(obj.IsRunning);
            };
            Action after = () =>
            {
                AfterCalled = true;
                Assert.IsFalse(obj.IsRunning);
            };
            var result =obj.RunFunc<string>(myAction, before, after);
            Assert.IsFalse(obj.IsRunning);
            Assert.IsTrue(myActionCalled);
            Assert.IsTrue(BeforeCalled);
            Assert.IsTrue(AfterCalled);
            Assert.AreEqual("ok_2", result);
        }
        //public TResult RunFunc<TResult>(Func<TResult> myFunc, Action before, Action after)
        //public TResult RunFunc<TResult>(Func<TResult> myFunc)

    }
}