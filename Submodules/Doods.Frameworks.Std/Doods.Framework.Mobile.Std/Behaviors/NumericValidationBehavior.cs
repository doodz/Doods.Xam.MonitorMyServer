using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Behaviors
{
    /// <summary>
    ///     A custom behavior for the Xamarin.Forms Entry control to
    ///     restrict the input to be numeric only in the form of a double or integer.
    /// </summary>
    public class NumericValidationBehavior : Behavior<Entry>
    {
        /// <summary>
        ///     Backing storage for the boolean flag which decides between
        ///     integer vs. double validation.
        /// </summary>
        public static BindableProperty AllowDecimalProperty = BindableProperty.Create(
            nameof(AllowDecimal),
            typeof(bool), typeof(NumericValidationBehavior),
            true);


        /// <summary>
        ///     Backing storage for the color used when the
        ///     Entry has invalid data (non-numeric).
        /// </summary>
        public static BindableProperty InvalidColorProperty = BindableProperty.Create(
            nameof(InvalidColor),
            typeof(Color), typeof(NumericValidationBehavior),
            Color.Red);

        /// <summary>
        ///     Bindable property to hold the boolean flag which decides
        ///     whether we test for integer vs. double values.
        /// </summary>
        /// <value>The selected item.</value>
        public bool AllowDecimal
        {
            get => (bool) GetValue(AllowDecimalProperty);
            set => SetValue(AllowDecimalProperty, value);
        }

        /// <summary>
        ///     Bindable property to hold the color used when the
        ///     Entry has invalid data (non-numeric).
        /// </summary>
        /// <value>The selected item.</value>
        public Color InvalidColor
        {
            get => (Color) GetValue(InvalidColorProperty);
            set => SetValue(InvalidColorProperty, value);
        }

        /// <summary>
        ///     Called when this behavior is attached to a visual.
        /// </summary>
        /// <param name="bindable">Visual owner</param>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        /// <summary>
        ///     Called when this behavior is detached from a visual
        /// </summary>
        /// <param name="bindable">Visual owner</param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        /// <summary>
        ///     Called when the associated Entry has new text.
        ///     This changes the text color to reflect whether the data
        ///     is valid.
        /// </summary>
        /// <param name="sender">Entry control</param>
        /// <param name="args">TextChanged event arguments</param>
        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var isValid = false;
            if (AllowDecimal)
                isValid = double.TryParse(args.NewTextValue, out var result);
            else
                isValid = long.TryParse(args.NewTextValue, out var result);

            ((Entry) sender).TextColor = isValid ? Color.Default : InvalidColor;
        }
    }
}