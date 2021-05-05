using System.ComponentModel;
using Doods.Framework.Std;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doods.Framework.TU
{
  
    internal class MyNotifyPropertyChangedBase : NotifyPropertyChangedBase
    {
        private string _myString;
        private int _myInt;
        private double _myDouble;

        public string MyString
        {
            get => _myString;
            set => SetProperty(ref _myString, value);
        }
        public int MyInt
        {
            get => _myInt;
            set => SetProperty(ref _myInt, value);
        }
        public double MyDouble
        {
            get => _myDouble;
            set => SetProperty(ref _myDouble, value);
        }
    }


    [TestClass]
    public class NotifyPropertyChangedBaseUnitTest
    {
        [TestMethod]
        public void Create()
        {
            var obj = new NotifyPropertyChangedBase();

            Assert.IsNotNull(obj);
            

        }
      

        [DataTestMethod]
        [DataRow(" ")]
        [DataRow(" _ ")]
        [DataRow("")]
        public void NotifyPropertyChangedStringUnitTest(string testval)
        {
            var propertyName = "";
            var change = new PropertyChangedEventArgs(propertyName);
            var obj = new MyNotifyPropertyChangedBase();
            obj.PropertyChanged += (sender, args) =>
            {
                propertyName = args.PropertyName;
            };

            obj.MyString = testval;
            Assert.IsNotNull(obj);
            Assert.AreEqual(nameof(MyNotifyPropertyChangedBase.MyString), propertyName);
            Assert.AreEqual(testval, obj.MyString);


        }

        [DataTestMethod]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
      
        public void NotifyPropertyChangedIntUnitTest(int testval)
        {
            var propertyName = "";
            var change = new PropertyChangedEventArgs(propertyName);
            var obj = new MyNotifyPropertyChangedBase();
            obj.PropertyChanged += (sender, args) =>
            {
                propertyName = args.PropertyName;
            };

            obj.MyInt = testval;
           
            Assert.IsNotNull(obj);
            Assert.AreEqual(nameof(MyNotifyPropertyChangedBase.MyInt), propertyName);
            Assert.AreEqual(testval, obj.MyInt);

        }

        [DataTestMethod]
        [DataRow(double.MaxValue)]
        [DataRow(double.MinValue)]

        public void NotifyPropertyChangedDoubleUnitTest(double testval)
        {
            var propertyName = "";
            var change = new PropertyChangedEventArgs(propertyName);
            var obj = new MyNotifyPropertyChangedBase();
            obj.PropertyChanged += (sender, args) =>
            {
                propertyName = args.PropertyName;
            };

            obj.MyDouble = testval;
            Assert.IsNotNull(obj);
            Assert.AreEqual(nameof(MyNotifyPropertyChangedBase.MyDouble), propertyName);
            Assert.AreEqual(testval, obj.MyDouble);
        }
    }
}