using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Doods.Framework.Std;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls
{
    public class EnumBindableDescriptionLocalizablePicker<T> : EnumBindableDescriptionPicker<T> where T : struct
    {
        public static readonly BindableProperty DescriptionsHasPrecedenceProperty =
            BindableProperty.Create(nameof(DescriptionHasPrecedence), typeof(bool),
                typeof(EnumBindableDescriptionLocalizablePicker<T>),
                false,
                propertyChanged: (bindable, value, newValue) =>
                {
                    ((EnumBindableDescriptionLocalizablePicker<T>) bindable).LoadList();
                });

        public static readonly BindableProperty TranslateProperty =
            BindableProperty.Create(nameof(Translate), typeof(ITranslateService),
                typeof(EnumBindableDescriptionLocalizablePicker<T>),
                propertyChanged: (bindable, value, newValue) =>
                {
                    ((EnumBindableDescriptionLocalizablePicker<T>) bindable).LoadList();
                });


        public ITranslateService Translate
        {
            get => (ITranslateService) GetValue(TranslateProperty);
            set => SetValue(TranslateProperty, value);
        }

        public bool DescriptionHasPrecedence
        {
            get => (bool) GetValue(DescriptionsHasPrecedenceProperty);
            set => SetValue(DescriptionsHasPrecedenceProperty, value);
        }

        internal override string ValueToString(object value)
        {
            return GetEnumDescription(value);
        }


        protected override string GetEnumDescription(object value)
        {
            var result = value.ToString();
            var attribute = typeof(T).GetRuntimeField(value.ToString()).GetCustomAttributes<DescriptionAttribute>(false)
                .SingleOrDefault();
            if (attribute != null)
            {
                if ((bool) GetValue(DescriptionsHasPrecedenceProperty))
                    return attribute.Description;
                var match2 = Translate?.Translate($"{attribute.Description}");
                return !string.IsNullOrWhiteSpace(match2) ? match2 : result;
            }

            var match = Translate?.Translate($"{typeof(T).Name}_{value}");
            //var match = Translate?.ProvideValue($"openmediavault::{value}");
            return !string.IsNullOrWhiteSpace(match) ? match : result;
        }
    }
}