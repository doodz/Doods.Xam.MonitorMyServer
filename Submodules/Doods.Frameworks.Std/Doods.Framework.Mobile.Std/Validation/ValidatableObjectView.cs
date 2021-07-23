using System;
using System.Windows.Input;
using Doods.Framework.Std.Validation;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Validation
{
    public class ValidatableObjectView<T> : ValidatableObject<T>
    {
        private Keyboard _keyboard = Keyboard.Default;
        private string _title;

        public ValidatableObjectView(string title, bool autoValidation, Keyboard keyboard) : this(title, autoValidation)
        {
            if (keyboard == null)
                throw new ArgumentNullException(nameof(keyboard));
            Keyboard = keyboard;
        }

        public ValidatableObjectView(string title, bool autoValidation) : base(autoValidation)
        {
            Title = title;
        }

        public ICommand ValidateCommand => new Command(() => Validate());
        public ICommand OnNextCommand => new Command(() => OnNext());

        public string Title
        {
            get => _title;
            private set => SetProperty(ref _title, value);
        }

        public Keyboard Keyboard
        {
            get => _keyboard;
            private set => SetProperty(ref _keyboard, value);
        }

        public void OnNext()
        {
            //var arg = new FocusRequestArgs { Focus = true };
            //FocusChangeRequested(this, arg);
            //return arg.Result;
            //NextEntry?.Focus();
        }
    }
}