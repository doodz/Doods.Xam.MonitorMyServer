using System;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Controls
{
    public class EnumBindablePicker<T> : Picker where T : struct
    {
        public new static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(T), typeof(EnumBindablePicker<T>),
                default(T), propertyChanged: OnSelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);

        public EnumBindablePicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
            LoadList();
        }

        public new T SelectedItem
        {
            get => (T) GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        protected internal void LoadList()
        {
            Items.Clear();
            foreach (var value in System.Enum.GetValues(typeof(T))) Items.Add(ValueToString(value));
            SelectedIndex = 0;
        }

        internal virtual string ValueToString(object value)
        {
            return value.ToString();
        }

        internal virtual void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            SelectedItem = SelectedIndex < 0 || SelectedIndex > Items.Count - 1
                ? default
                : (T) System.Enum.Parse(typeof(T), Items[SelectedIndex]);
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null) return;
            if (bindable is EnumBindablePicker<T> picker)
                picker.SelectedIndex =
                    picker.Items.IndexOf(newvalue.ToString());
        }
    }
}