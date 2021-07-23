using System;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Resources.Styles;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Helpers
{
   
    public static class ThemeHelper
    {
        public static Theme CurrentTheme
        {
            get => (Theme) Preferences.Get(nameof(CurrentTheme),
                HasDefaultThemeOption ? (int) Theme.Default : (int) Theme.Light);
            set => Preferences.Set(nameof(CurrentTheme), (int) value);
        }

        public static bool HasDefaultThemeOption
        {
            get
            {
                var minDefaultVersion = new Version(13, 0);
                if (DeviceInfo.Platform == DevicePlatform.UWP)
                    minDefaultVersion = new Version(10, 0, 17763, 1);
                else if (DeviceInfo.Platform == DevicePlatform.Android)
                    minDefaultVersion = new Version(10, 0);

                return DeviceInfo.Version >= minDefaultVersion;
            }
        }

        public static void ChangeTheme(Theme theme, bool forceTheme = false)
        {
            // don't change to the same theme
            if (theme == CurrentTheme && !forceTheme)
                return;

            //// clear all the resources
            var applicationResourceDictionary = Application.Current.Resources;

            if (theme == Theme.Default)
                theme = AppInfo.RequestedTheme == AppTheme.Dark ? Theme.Dark : Theme.Light;


            ResourceDictionary newTheme = theme switch
            {
                Theme.Light => new LightTheme(),
                Theme.Dark => new DarkTheme(),
                _ => new LightTheme()
            };
            ManuallyCopyThemes(newTheme, applicationResourceDictionary);

            CurrentTheme = theme;


            //var background = (Color)App.Current.Resources["PrimaryDarkColor"];
            //var environment = DependencyService.Get<IEnvironment>();
            //environment?.SetStatusBarColor(ColorConverters.FromHex(background.ToHex()), false);
        }

        private static void ManuallyCopyThemes(ResourceDictionary fromResource, ResourceDictionary toResource)
        {
            foreach (var item in fromResource.Keys) toResource[item] = fromResource[item];
        }
    }
}