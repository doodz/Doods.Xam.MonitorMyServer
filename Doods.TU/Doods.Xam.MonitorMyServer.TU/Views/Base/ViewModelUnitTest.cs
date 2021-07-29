using Autofac.Extras.Moq;
using Doods.Xam.MonitorMyServer.Views.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Xam.MonitorMyServer.TU.Views.Base
{
    [TestClass]
    public class ViewModelUnitTest
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            //var builder = new ContainerBuilder();

            //var logger = new Mock<ILogger>();
            //var telemetryService = new Mock<ITelemetryService>();
            //builder.RegisterInstance(logger.Object).As<ILogger>();
            //builder.RegisterInstance(telemetryService.Object).As<ITelemetryService>();
            
            //var container = new Mock<IContainer>();

            //container.Setup(c => c.Resolve()).Returns(AutoMock.GetLoose().Create<>()
               
            //AutoMock.GetLoose();

            var toto = AutoMock.GetLoose();
            App.SetContainer(toto.Container);
            
          
           
            // Executes once before the test run. (Optional)
        }
        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            // Executes once for the test class. (Optional)
        }
        [TestInitialize]
        public void Setup()
        {
            // Runs before each test. (Optional)
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
        public void Create()
        {
            var obj = (ViewModel)new ViewModelWhithState();
            Assert.IsNotNull(obj);
          

        }
    }
}