using Autofac.Extras.Moq;
using Doods.Xam.MonitorMyServer.TU.MockForms;
using Doods.Xam.MonitorMyServer.Views.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var localAutoMock = AutoMock.GetLoose();
            App.SetContainer(localAutoMock.Container);
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
          

        }
    }
}