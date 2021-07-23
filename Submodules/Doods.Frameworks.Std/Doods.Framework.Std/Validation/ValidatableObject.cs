using System;
using System.Collections.Generic;
using System.Linq;

namespace Doods.Framework.Std.Validation
{
    public class ValidatableObject<T> : NotifyPropertyChangedBase, IValidity
    {
        private ICollection<string> _errors = new List<string>();
        private T _innerValue;
        private bool _isValid = true;
        private ValidatableObjectStatus _status = ValidatableObjectStatus.Valid;

        public ValidatableObject(bool autoValidation)
        {
            AutoValidation = autoValidation;
        }

        public IList<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();

        public ICollection<string> Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }

        public string FirstError => Errors.FirstOrDefault();

        public bool AutoValidation { get; set; }

        public T Value
        {
            get => _innerValue;
            set
            {
                if (SetProperty(ref _innerValue, value) && AutoValidation) Validate();
            }
        }

        public ValidatableObjectStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public event EventHandler StatusChanged;

        public bool Validate()
        {
            Errors.Clear();
            Errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage).ToList();
            IsValid = !Errors.Any();
            Status = IsValid ? ValidatableObjectStatus.Valid : ValidatableObjectStatus.Error;
            OnPropertyChanged(nameof(FirstError));
            StatusChanged?.Invoke(this, EventArgs.Empty);

            return IsValid;
        }
    }
}