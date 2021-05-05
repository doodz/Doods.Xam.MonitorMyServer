using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Doods.Framework.TU.MobileMvvm
{
    public class MyViewModelBase : ViewModelBase
    {
        public MyViewModelBase(ILogger logger, ITelemetryService telemetryService) : base(logger, telemetryService)
        {
        }

        public override IColorPalette ColorPalette { get; }
    }

    [TestClass]
    public class ViewModelBaseUnitTest
    {

        [TestMethod]
        public void Create()
        {
            var obj = new MyViewModelBase(null,null);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ViewModelState);
            Assert.IsNotNull(obj.RefreshCmd);

            Assert.IsNull(obj.ColorPalette);
            Assert.IsNull(obj.CmdState);
           
            Assert.IsNull(obj.Title);
            Assert.IsFalse(obj.IsBusy);
            Assert.IsFalse(obj.IsLoaded);
            Assert.IsFalse(obj.IsLoading);
            Assert.IsFalse(obj.IsVisible);
            Assert.AreEqual(ViewModelState.None, obj.ViewModelState);
        }


        [TestMethod]
        public void CreateMock()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ViewModelState);
            Assert.IsNotNull(obj.RefreshCmd);

            Assert.IsNull(obj.ColorPalette);
            Assert.IsNull(obj.CmdState);

            Assert.IsNull(obj.Title);
            Assert.IsFalse(obj.IsBusy);
            Assert.IsFalse(obj.IsLoaded);
            Assert.IsFalse(obj.IsLoading);
            Assert.IsFalse(obj.IsVisible);
            Assert.AreEqual(ViewModelState.None, obj.ViewModelState);
        }

        [TestMethod]
        public void SetIsLoaded_true()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
          
            obj.IsLoaded = true;
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ViewModelState);
            Assert.IsNotNull(obj.RefreshCmd);

            Assert.IsNull(obj.ColorPalette);
            Assert.IsNull(obj.CmdState);

            Assert.IsNull(obj.Title);
            Assert.IsFalse(obj.IsBusy);
            Assert.IsTrue(obj.IsLoaded);
            Assert.IsFalse(obj.IsLoading);
            Assert.IsFalse(obj.IsVisible);
            Assert.AreEqual(ViewModelState.None, obj.ViewModelState);
        }
        [TestMethod]
        public void SetIsVisible_true()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
          
            obj.IsVisible = true;

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ViewModelState);
            Assert.IsNotNull(obj.RefreshCmd);

            Assert.IsNull(obj.ColorPalette);
            Assert.IsNull(obj.CmdState);

            Assert.IsNull(obj.Title);
            Assert.IsFalse(obj.IsBusy);
            Assert.IsFalse(obj.IsLoaded);
            Assert.IsFalse(obj.IsLoading);
            Assert.IsTrue(obj.IsVisible);

            Assert.AreEqual(ViewModelState.None, obj.ViewModelState);
        }

        [TestMethod]
        public void GetToolBarItemDescriptions_null()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);

            var result =obj.GetToolBarItemDescriptions();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CanUpdateToolBar()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
            var result = obj.CanUpdateToolBar();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CancelExecutions()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
            obj.CancelExecutions();
            Assert.IsNotNull(obj);
            Assert.AreEqual(ViewModelState.None, obj.ViewModelState);
        }
        [TestMethod]
        public async  Task OnDisappearingAsync()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
            await obj.OnDisappearingAsync();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.IsVisible);
            Assert.AreEqual(ViewModelState.None, obj.ViewModelState);
        }

        [TestMethod]
        public  void RefreshCmd_CanExecute_true()
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
            var reult = obj.RefreshCmd.CanExecute(null);
            Assert.IsTrue(reult);

        }

        [TestMethod]
        public void RefreshCmd_Execute()//TODO async run
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);
            
            var lst = new List<string>();
            obj.PropertyChanged += (sender, args) =>
            {
                lst.Add(args.PropertyName);
            };
            obj.RefreshCmd.Execute(null);
            //IsLoading event
            //ViewModelState event
            Assert.IsTrue(lst.Contains(nameof(MyViewModelBase.IsLoading)));
            Assert.IsTrue(lst.Contains(nameof(MyViewModelBase.ViewModelState)));
            Assert.IsTrue(lst.Contains(nameof(MyViewModelBase.IsBusy)));
            Assert.IsTrue(lst.Contains("BusyCount"));
            Assert.AreEqual(4,lst.Count);
            Assert.AreEqual(ViewModelState.Loading, obj.ViewModelState);
        }
        [TestMethod]
        public async Task RefreshCmd_Execute_Wait500()//TODO async run
        {
            var loggertMock = new Mock<ILogger>();
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var obj = new MyViewModelBase(loggertMock.Object, telemetryServiceMock.Object);

            var lst = new List<string>();
            obj.PropertyChanged += (sender, args) =>
            {
                lst.Add(args.PropertyName);
            };
            obj.RefreshCmd.Execute(null);
           

            await Task.Delay(TimeSpan.FromMilliseconds(500));
            Assert.IsTrue(lst.Contains(nameof(MyViewModelBase.IsLoading)));
            Assert.IsTrue(lst.Contains(nameof(MyViewModelBase.ViewModelState)));
            Assert.IsTrue(lst.Contains(nameof(MyViewModelBase.IsBusy)));
            Assert.IsTrue(lst.Contains("BusyCount"));
            Assert.AreEqual(9, lst.Count);
            Assert.AreEqual(ViewModelState.Loaded, obj.ViewModelState);
        }
    }
}
