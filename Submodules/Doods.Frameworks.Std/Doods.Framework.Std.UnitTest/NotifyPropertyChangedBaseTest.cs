using Doods.Framework.Std.UnitTest.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace Doods.Framework.Std.UnitTest
{
    [TestClass]
    public class NotifyPropertyChangedBaseTest
    {
        private ObservableObject _observableObject;
        [TestInitialize]
        public void Setup()
        {
            _observableObject = new ObservableObject();
            _observableObject.PropertyOne = "Doods";
            _observableObject.PropertyTwo = 42;
        }

        [TestMethod]
        public void OnPropertyChanged()
        {
            PropertyChangedEventArgs updated = null;
            _observableObject.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            _observableObject.PropertyOne = "Framework";


            Assert.IsNotNull(updated, "Property changed didn't raise");
            Assert.AreEqual(updated.PropertyName, nameof(_observableObject.PropertyOne), "Correct Property name didn't get raised");
        }

        [TestMethod]
        public void OnDidntChange()
        {
            PropertyChangedEventArgs updated = null;
            _observableObject.PropertyChanged += (sender, args) =>
            {
                updated = args;
            };

            _observableObject.PropertyOne = "Doods";
            _observableObject.PropertyTwo = 42;

            Assert.IsNull(updated, "Property changed was raised, but shouldn't have been");
        }

        [TestMethod]
        public void OnChangedEvent()
        {

            var triggered = false;
            _observableObject.Changed = () =>
            {
                triggered = true;
            };

            _observableObject.PropertyOne = "Framework";

            Assert.IsTrue(triggered, "OnChanged didn't raise");
        }

        [TestMethod]
        public void ValidateEvent()
        {
            var contol = "Framework";
            var triggered = false;
            _observableObject.Validate = (oldValue, newValue) =>
            {
                triggered = true;
                return oldValue != newValue;
            };

            _observableObject.PropertyOne = contol;

            Assert.IsTrue(triggered, "ValidateValue didn't raise");
            Assert.AreEqual(_observableObject.PropertyOne, contol, "Value was not set correctly.");

        }

        [TestMethod]
        public void NotValidateEvent()
        {
            var contol = _observableObject.PropertyOne;
            var triggered = false;
            _observableObject.Validate = (oldValue, newValue) =>
            {
                triggered = true;
                return false;
            };

            _observableObject.PropertyOne = "Framework";

            Assert.IsTrue(triggered, "ValidateValue didn't raise");
            Assert.AreEqual(_observableObject.PropertyOne, contol, "Value should not have been set.");

        }

        [TestMethod]
        public void ValidateEventException()
        {
            _observableObject.Validate = (oldValue, newValue) =>
            {
                throw new ArgumentOutOfRangeException();
                return false;
            };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _observableObject.PropertyOne = "Framework", "Should throw ArgumentOutOfRangeException");

        }
    }
}
