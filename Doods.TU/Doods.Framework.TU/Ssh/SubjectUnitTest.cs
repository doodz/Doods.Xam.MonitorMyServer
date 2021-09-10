using Doods.Framework.Ssh.Std;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Ssh
{
    [TestClass]
    public class SubjectUnitTest
    {
        [TestMethod]
        public void Create()
        {
         
            var obj = new Subject<string>();
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj,typeof(ISubject<string>));
           
            
        }
    }
}