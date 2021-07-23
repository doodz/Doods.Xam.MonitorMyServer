using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Doods.Framework.Mobile.Std.Controls
{
    public class EnumBindableDescriptionPicker<T> : EnumBindablePicker<T> where T : struct
    {
        internal override string ValueToString(object value)
        {
            return GetEnumDescription(value);
        }


        internal override void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = default;
                return;
            }

            if (!System.Enum.TryParse(Items[SelectedIndex], out T match))
                match = GetEnumByDescription(Items[SelectedIndex]);

            SelectedItem = (T) System.Enum.Parse(typeof(T), match.ToString());
        }

        protected virtual string GetEnumDescription(object value)
        {
            var result = value.ToString();
            var attribute = typeof(T).GetRuntimeField(result).GetCustomAttributes<DescriptionAttribute>(false)
                .SingleOrDefault();
            return attribute != null ? attribute.Description : result;
        }

        private T GetEnumByDescription(string description)
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>()
                .FirstOrDefault(x => string.Equals(GetEnumDescription(x), description));
        }
    }
}