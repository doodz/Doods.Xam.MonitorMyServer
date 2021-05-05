using Doods.Framework.Std;
using Doods.Framework.Std.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doods.Framework.TU.Validation
{
    [TestClass]
    public class ValidatableObjectUnitTest
    {
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Create(bool autovalidation)
        {
            var obj = new ValidatableObject<string>(autovalidation);

            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(0, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(0, obj.Validations.Count);
            Assert.IsNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Valid, obj.Status);
            Assert.IsTrue(obj.IsValid);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        [DataRow(true, false)]
        [DataRow(false, true)]
        public void SetAutoValidationValue(bool autovalidation, bool changeValue)
        {
            var obj = new ValidatableObject<string>(autovalidation)
            {
                AutoValidation = changeValue
            };

            Assert.IsNotNull(obj);
            Assert.AreEqual(changeValue, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(0, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(0, obj.Validations.Count);
            Assert.IsNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Valid, obj.Status);
            Assert.IsTrue(obj.IsValid);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        [DataRow(true, false)]
        [DataRow(false, true)]
        public void TestAddMockValidation(bool autovalidation, bool validation)
        {
            var obj = new ValidatableObject<string>(autovalidation);

            var mock = new Mock<IValidationRule<string>>();

            mock.Setup(m => m.ValidationMessage).Returns($"My mock validation {validation}");
            mock.Setup(m => m.Check(It.IsAny<string>())).Returns(validation);

            obj.Validations.Add(mock.Object);

            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(0, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.IsNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Valid, obj.Status);
            Assert.IsTrue(obj.IsValid);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        [DataRow(false, true)]
        public void TestAddValueMockValidation(bool autovalidation, bool validation)
        {
            var obj = new ValidatableObject<string>(autovalidation);

            var mock = new Mock<IValidationRule<string>>();

            mock.Setup(m => m.ValidationMessage).Returns($"My mock validation {validation}");
            mock.Setup(m => m.Check(It.IsAny<string>())).Returns(validation);

            obj.Validations.Add(mock.Object);
            obj.Value = "test";
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(0, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.IsNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Valid, obj.Status);
            Assert.IsTrue(obj.IsValid);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(true, false)]
        public void TestAddValueMockAutoValidation(bool autovalidation, bool validation)
        {
            var obj = new ValidatableObject<string>(autovalidation);

            var mock = new Mock<IValidationRule<string>>();

            mock.Setup(m => m.ValidationMessage).Returns($"My mock validation {validation}");
            mock.Setup(m => m.Check(It.IsAny<string>())).Returns(validation);

            obj.Validations.Add(mock.Object);
            obj.Value = "test";
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(validation ? 0 : 1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.AreEqual(validation ? null : $"My mock validation {validation}", obj.FirstError);
            Assert.AreEqual(validation ? ValidatableObjectStatus.Valid : ValidatableObjectStatus.Error, obj.Status);
            Assert.AreEqual(validation, obj.IsValid);
        }

        [DataTestMethod]
        [DataRow(false, true)]
        [DataRow(false, false)]
        public void TestValidateWithMockAutoValidationFalse(bool autovalidation, bool validation)
        {
            var obj = new ValidatableObject<string>(autovalidation);

            var mock = new Mock<IValidationRule<string>>();

            mock.Setup(m => m.ValidationMessage).Returns($"My mock validation {validation}");
            mock.Setup(m => m.Check(It.IsAny<string>())).Returns(validation);

            obj.Validations.Add(mock.Object);
            obj.Value = "test";
            var result = obj.Validate();
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(validation ? 0 : 1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.AreEqual(validation ? null : $"My mock validation {validation}", obj.FirstError);
            Assert.AreEqual(validation ? ValidatableObjectStatus.Valid : ValidatableObjectStatus.Error, obj.Status);
            Assert.AreEqual(validation, obj.IsValid);
            Assert.AreEqual(validation, result);
        }


        [DataTestMethod]
        public void TestValidateWithMockAutoValidationTrueThenFase()
        {
            var autovalidation = true;

            var obj = new ValidatableObject<string>(autovalidation);

            var mock = new Mock<IValidationRule<string>>();

            mock.Setup(m => m.ValidationMessage).Returns("My mock validation test 1");


            mock.SetupSequence(m => m.Check(It.IsAny<string>()))
                .Returns(true)
                .Returns(false);

            obj.Validations.Add(mock.Object);
            obj.Value = "test 1";
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(0, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.IsNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Valid, obj.Status);
            Assert.IsTrue(obj.IsValid);


            obj.Value = "test";
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.AreEqual("My mock validation test 1", obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Error, obj.Status);
            Assert.IsFalse(obj.IsValid);
        }

        [DataTestMethod]
        public void TestValidateWithMockAutoValidationFalseThenTrue()
        {
            var autovalidation = true;

            var obj = new ValidatableObject<string>(autovalidation);

            var mock = new Mock<IValidationRule<string>>();

            mock.Setup(m => m.ValidationMessage).Returns("My mock validation test 1");
            mock.SetupSequence(m => m.Check(It.IsAny<string>()))
                .Returns(false)
                .Returns(true);

            obj.Validations.Add(mock.Object);
            obj.Value = "test 1";
            //var result = obj.Validate();
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.AreEqual("My mock validation test 1", obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Error, obj.Status);
            Assert.IsFalse(obj.IsValid);
            //Assert.IsFalse(result);

            obj.Value = "test";
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(0, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.IsNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Valid, obj.Status);
            Assert.IsTrue(obj.IsValid);
        }

        [DataTestMethod]
        public void TestValidateWithMockAutoValidationFalseThenFalse()
        {
            var autovalidation = true;

            var obj = new ValidatableObject<string>(autovalidation);

            var mock = new Mock<IValidationRule<string>>();

            mock.Setup(m => m.ValidationMessage).Returns("My mock validation test 1");
            mock.SetupSequence(m => m.Check(It.IsAny<string>()))
                .Returns(false)
                .Returns(false);

            obj.Validations.Add(mock.Object);
            obj.Value = "test 1";
            //var result = obj.Validate();
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.AreEqual("My mock validation test 1", obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Error, obj.Status);
            Assert.IsFalse(obj.IsValid);
            //Assert.IsFalse(result);

            obj.Value = "test";
            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(1, obj.Validations.Count);
            Assert.AreEqual("My mock validation test 1", obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Error, obj.Status);
            Assert.IsFalse(obj.IsValid);
        }

        [DataTestMethod]
        public void TestValidateWithMockAutoValidationTrueAndFalse()
        {
            var autovalidation = true;

            var obj = new ValidatableObject<string>(autovalidation);

            var mockTrue = new Mock<IValidationRule<string>>();
            mockTrue.Setup(m => m.ValidationMessage).Returns("My mock validation test true");
            mockTrue.Setup(m => m.Check(It.IsAny<string>())).Returns(true);

            var mockFalse = new Mock<IValidationRule<string>>();
            mockFalse.Setup(m => m.ValidationMessage).Returns("My mock validation test false");
            mockFalse.Setup(m => m.Check(It.IsAny<string>())).Returns(false);
            obj.Validations.Add(mockTrue.Object);
            obj.Validations.Add(mockFalse.Object);
            obj.Value = "test 1";

            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(2, obj.Validations.Count);
            Assert.IsNotNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Error, obj.Status);
            Assert.IsFalse(obj.IsValid);
        }


        [DataTestMethod]
        public void TestValidateWithMockAutoValidationFalseEndTrue()
        {
            var autovalidation = true;

            var obj = new ValidatableObject<string>(autovalidation);

            var mockTrue = new Mock<IValidationRule<string>>();
            mockTrue.Setup(m => m.ValidationMessage).Returns("My mock validation test true");
            mockTrue.Setup(m => m.Check(It.IsAny<string>())).Returns(true);

            var mockFalse = new Mock<IValidationRule<string>>();
            mockFalse.Setup(m => m.ValidationMessage).Returns("My mock validation test false");
            mockFalse.Setup(m => m.Check(It.IsAny<string>())).Returns(false);
            obj.Validations.Add(mockFalse.Object);
            obj.Validations.Add(mockTrue.Object);
            obj.Value = "test 1";

            Assert.IsNotNull(obj);
            Assert.AreEqual(autovalidation, obj.AutoValidation);
            Assert.IsNotNull(obj.Errors);
            Assert.AreEqual(1, obj.Errors.Count);
            Assert.IsNotNull(obj.Validations);
            Assert.AreEqual(2, obj.Validations.Count);
            Assert.IsNotNull(obj.FirstError);
            Assert.AreEqual(ValidatableObjectStatus.Error, obj.Status);
            Assert.IsFalse(obj.IsValid);
        }


        // true and true
        // true and false
        // false and false
        // false and true

        // clear
        // EventHandle
    }
}