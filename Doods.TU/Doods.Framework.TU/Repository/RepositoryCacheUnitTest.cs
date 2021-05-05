using System;
using System.Collections.Generic;
using Doods.Framework.Repository.Std;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SQLite;

namespace Doods.Framework.TU.Repository
{
    [TestClass]
    public class TableBaseUnitTest
    {
        //public const long DefaultId = -1;

        //public DateTimeOffset SyncDate { get; set; }

        //[PrimaryKey] [AutoIncrement] public long? Id { get; set; }
        [TestMethod]
        public void Create()
        {
            var obj = new TableBase();
            Assert.IsNotNull(obj);
            Assert.AreEqual(-1, TableBase.DefaultId);
            Assert.AreEqual(default(DateTimeOffset), obj.SyncDate);
            Assert.IsNull(obj.Id);
           
        }

        [TestMethod]
        public void SetDateTimeOffset()
        {
            var obj = new TableBase();
            Assert.IsNotNull(obj);
            var date = DateTimeOffset.MaxValue;
            obj.SyncDate = date;
            Assert.AreEqual(-1, TableBase.DefaultId);
            Assert.AreEqual(date, obj.SyncDate);
            Assert.IsNull(obj.Id);

        }
        [DataTestMethod]
        [DataRow(long.MinValue)]
        [DataRow(long.MaxValue)]
        [DataRow(0L)]
        [DataRow(1L)]
        [DataRow(null)]
        public void SetId(long? val)
        {
            var obj = new TableBase();
            obj.Id = val;
            Assert.IsNotNull(obj);
            Assert.AreEqual(-1, TableBase.DefaultId);
            Assert.AreEqual(default(DateTimeOffset), obj.SyncDate);
            Assert.AreEqual(val,obj.Id);
        }
    }

    [TestClass]
    public class RepositoryCacheUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new RepositoryCache();
            Assert.IsNotNull(obj);
        }

        [DataTestMethod]
        [DataRow("foo","bar")]
        [DataRow("_int", 1)]
        [DataRow("_bool", false)]
        [DataRow("_null", null)]
        [DataRow(null, null)]
        public void Set(string key,object val)
        {
            var dictionary = new Dictionary<string, string>();
            var watcherMock = new Mock<IWatcher>();
            watcherMock.Setup(m => m.Properties)
                .Returns(dictionary);
            //watcherMock.Setup(r => r.Properties.Add(It.Is<string>(r => r == "key"), It.Is<string>(r => r == key)));

            //watcher?.Properties?.Add("key", k);


            var timeWatcherMock = new Mock<ITimeWatcher>();
           

            timeWatcherMock.Setup(r => r.StartWatcher(It.IsAny<string>(),It.IsAny<bool>()))
                .Returns(() => watcherMock.Object);
            
            var obj = new RepositoryCache();
            Assert.IsNotNull(obj);
            obj.Set(timeWatcherMock.Object,key,val);
            timeWatcherMock.Verify(foo => foo.StartWatcher("RepositoryCache.Set", It.IsAny<bool>()), Times.Once());
            //watcherMock.Verify(r => r.Properties.Add(It.Is<string>(r => r == "key"), It.Is<string>(r => r == key)), Times.Once());
            watcherMock.VerifyGet(r => r.Properties, Times.Once());
            var b = dictionary.ContainsValue(key);
            Assert.IsTrue(b);
            //Assert.AreEqual(val,dictionary[key]);
        }

        [DataTestMethod]

        [DataRow(null, null)]
        public void GetNull(string key, object val)
        {
            var dictionary = new Dictionary<string, string>();
            var watcherMock = new Mock<IWatcher>();
            watcherMock.Setup(m => m.Properties)
                .Returns(dictionary);
            //watcherMock.Setup(r => r.Properties.Add(It.Is<string>(r => r == "key"), It.Is<string>(r => r == key)));

            //watcher?.Properties?.Add("key", k);


            var timeWatcherMock = new Mock<ITimeWatcher>();


            timeWatcherMock.Setup(r => r.StartWatcher(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(() => watcherMock.Object);

            var obj = new RepositoryCache();
            Assert.IsNotNull(obj);
            obj.Set(timeWatcherMock.Object, key, val);
            timeWatcherMock.Verify(foo => foo.StartWatcher("RepositoryCache.Set", It.IsAny<bool>()), Times.Once());
            //watcherMock.Verify(r => r.Properties.Add(It.Is<string>(r => r == "key"), It.Is<string>(r => r == key)), Times.Once());
            watcherMock.VerifyGet(r => r.Properties, Times.Once());
            var b = dictionary.ContainsValue(key);
            Assert.IsTrue(b);
            //Assert.AreEqual(val,dictionary[key]);
        }

        [DataTestMethod]
        [DataRow("foo", "bar")]
        [DataRow("_int", 1)]
        [DataRow("_bool", false)]
        [DataRow("_null", null)]
        [DataRow(null, null)]
        public void Get(string key, object val)
        {
            var dictionary = new Dictionary<string, string>();
            var watcherMock = new Mock<IWatcher>();
            watcherMock.Setup(m => m.Properties)
                .Returns(dictionary);
            var timeWatcherMock = new Mock<ITimeWatcher>();


            timeWatcherMock.Setup(r => r.StartWatcher(It.IsAny<string>(), It.IsAny<bool>()))
                .Callback(() => dictionary.Clear())
                .Returns(() => watcherMock.Object);

            var obj = new RepositoryCache();
            Assert.IsNotNull(obj);
            obj.Set(timeWatcherMock.Object, key, val);

            var result = obj.Get<object>(timeWatcherMock.Object, key);
            timeWatcherMock.Verify(foo => foo.StartWatcher("RepositoryCache.Get", It.IsAny<bool>()), Times.Once());
            Assert.AreEqual(val, result);
        }
        //[TestMethod]
        //public void Clear()
        //{
        //    var timeWatcherMock = new Mock<ITimeWatcher>();
        //    var watcherMock = new Mock<IWatcher>();
        //    timeWatcherMock.Setup(r => r.StartWatcher(It.IsAny<string>(), It.IsAny<bool>()))
        //        .Returns(() => watcherMock.Object);
        //    var obj = new RepositoryCache();
        //    Assert.IsNotNull(obj);
        //    var result = obj.Clear(timeWatcherMock.Object);
        //    Assert.IsTrue(result);
        //    timeWatcherMock.Verify(foo => foo.StartWatcher("RepositoryCache.Clear", It.IsAny<bool>()), Times.Once());
        //}
        [TestMethod]
        public void ClearAll()
        {
            var timeWatcherMock = new Mock<ITimeWatcher>();
            var watcherMock = new Mock<IWatcher>();
            timeWatcherMock.Setup(r => r.StartWatcher(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(() => watcherMock.Object);
            var obj = new RepositoryCache();
            Assert.IsNotNull(obj);
            var result =obj.ClearAll(timeWatcherMock.Object);
            Assert.IsTrue(result);
            timeWatcherMock.Verify(foo => foo.StartWatcher("RepositoryCache.ClearAll", It.IsAny<bool>()), Times.Once());
        }
    }
}
