using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Widget;
using Doods.Framework.Mobile.Std.Behaviors;
using Doods.Framework.Mobile.Std.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Doods.MonitorMyServer")]
[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]

namespace Doods.Xam.MonitorMyServer.Droid.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        private EditText control;

        protected override void OnAttached()
        {
            try
            {
                control = Control as EditText;
                UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorBehavior.LineColorProperty.PropertyName) UpdateLineColor();
        }

        private void UpdateLineColor()
        {
            try
            {
                if (control != null)
                    control.Background.SetColorFilter(LineColorBehavior.GetLineColor(Element).ToAndroid(),
                        PorterDuff.Mode.SrcAtop);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}