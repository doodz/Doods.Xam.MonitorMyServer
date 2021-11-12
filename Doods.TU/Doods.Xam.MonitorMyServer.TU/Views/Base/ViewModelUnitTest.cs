using System.Threading;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.TU.MockForms;
using Doods.Xam.MonitorMyServer.Views.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Xam.MonitorMyServer.TU
{

    [TestClass]
    public class BaselUnitTest
    {
        public BaselUnitTest()
        {
            var localAutoMock = AutoMock.GetLoose();
            App.SetContainer(localAutoMock.Container);
            MockPlatformServices.Init();
        }
        protected static AutoMock LocalAutoMock;
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // Executes once before the test run. (Optional)
            //var localAutoMock = AutoMock.GetLoose();
            //App.SetContainer(localAutoMock.Container);
            //MockPlatformServices.Init();

        }
        protected static void SetMockContainer()
        {
            App.SetContainer(LocalAutoMock.Container);
        }
        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            
            // Executes once for the test class. (Optional)
        }
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Executes once after the test run. (Optional)
        }
        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            // Runs once after all tests in this class are executed. (Optional)
            // Not guaranteed that it executes instantly after all tests from the class.
            //var localAutoMock = AutoMock.GetLoose();
            //App.SetContainer(localAutoMock.Container);
        }
    }


    [TestClass]
    public class ViewModelUnitTest: BaselUnitTest
    {
      

       

       
        [TestInitialize]
        public void Setup()
        {
            // Runs before each test. (Optional)
        }
      
        [TestCleanup]
        public void TearDown()
        {
            // Runs after each test. (Optional)
        }
        // Mark that this is a unit test method. (Required)
        [TestMethod]
        public void YouTestMethod()
        {
            // Your test code goes here.
        }

        [TestMethod]
        public void CreateViewModel()
        {
            var obj = (ViewModel)new ViewModelWhithState();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IViewModel));
            Assert.IsNotNull(obj.RefreshCmd);
            Assert.IsFalse(obj.IsLoading);
            Assert.IsFalse(obj.IsLoaded);
            Assert.IsFalse(obj.IsBusy);
            Assert.IsFalse(obj.IsVisible);
        }

        [TestMethod]
        public void ResolveColorPalette()
        {
            var obj = (ViewModel)new ViewModelWhithState();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.ColorPalette);
        }
       
        //[TestMethod]
        //public void ResolveNavigationService()
        //{
        //    var obj = (ViewModel)new ViewModelWhithState();
        //    Assert.IsNotNull(obj);
        //    Assert.IsNotNull(obj.NavigationService);
           
               
        //}

        [TestMethod]
        public async Task CallOnAppearingAsync()
        {
            var mokB = new Mock<IWatcher>();
            mokB.SetupAllProperties();
            var mockA = new Mock<ITimeWatcher>();
            mockA.Setup(x => x.StartWatcher(It.IsAny<string>(),It.IsAny<bool>())).Returns(() =>
                mokB.Object);
            LocalAutoMock = AutoMock.GetLoose(cfg => cfg.RegisterMock(mockA));
            SetMockContainer();

            var token = new CancellationToken(false);
            LoadingContext.FromUser = LoadingContext.Create(LoadingContext.FromUser, token, mockA.Object);
            LoadingContext.OnAppearing = LoadingContext.Create(LoadingContext.OnAppearing, token, mockA.Object);
            LoadingContext.RefreshVisual = LoadingContext.Create(LoadingContext.RefreshVisual, token, mockA.Object);

            var obj = (ViewModel)new ViewModelWhithState();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.IsVisible);
            await obj.OnAppearingAsync();
            Assert.IsTrue(obj.IsVisible);
            Assert.IsFalse(obj.IsLoading);
            mockA.Verify(x => x.StartWatcher(It.IsAny<string>(), It.IsAny<bool>()),Times.Once);
        }
    }
}