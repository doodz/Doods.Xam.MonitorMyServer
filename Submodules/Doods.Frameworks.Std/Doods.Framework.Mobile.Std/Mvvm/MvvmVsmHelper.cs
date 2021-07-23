using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Mvvm
{
    public class MvvmVsmHelper : BindableObject
    {
        public static readonly BindableProperty StateProperty = BindableProperty.CreateAttached("State", typeof(string),
            typeof(MvvmVsmHelper), "Normal", BindingMode.TwoWay, null, StatePropertyChanged);

        public static void SetState(BindableObject view, string value)
        {
            view.SetValue(StateProperty, value);
        }

        public static string GetState(BindableObject view)
        {
            return (string) view.GetValue(StateProperty);
        }

        private static void StatePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            try
            {
                var newState = (string) newvalue;
                Debug.WriteLine($"Tentative de changement de '{oldvalue}' vers '{newState}' sur '{bindable}'");
                if (newvalue == null) return;
                if (!VisualStateManager.GoToState((VisualElement) bindable, newState))
                    Debug.WriteLine($"Impossible de changer l'état '{oldvalue}' en '{newvalue}' sur '{bindable}'");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "; " + e.StackTrace);
                throw;
            }
        }
    }
}