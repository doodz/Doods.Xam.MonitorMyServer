using Doods.Framework.Repository.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Repository
{
    [TestClass]
    public class RepositoryBaseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var database = new Mock<IDatabase>();
            var repositoryCache = new Mock<IRepositoryCache>();
            
            var obj = new RepositoryBase(database.Object, repositoryCache.Object);
            Assert.IsNotNull(obj);
        }
    }
}