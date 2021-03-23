using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.Tests;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.About
{
    public class AboutPageViewModel : ViewModel
    {
        //https://play.google.com/store/ereview?origin=https://play.google.com&docId=com.doods.monitormyserver
        private const string Regexp = "<PackageReference Include=\"(.*)\" Version=\"(.*)\" />";

        private readonly Regex rx = new Regex(Regexp,
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public AboutPageViewModel()
        {
            ShowTestsPageCmd = new Command(() => NavigationService.NavigateAsync(nameof(TestPage)));
            DisplaySettingsPage = new Command(AppInfo.ShowSettingsUI);
            List = new List<string>();
            var rx = new Regex(Regexp,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Doods.Xam.MonitorMyServer.Resx.ThirdPartyLicenseOverview_doods.txt";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();

                var resultArray = result.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in resultArray)
                {
                    var matches = rx.Matches(s);
                    foreach (Match match in matches)
                    {
                        var groups = match.Groups;
                        var nuget = $"{groups[1].Value} {groups[2].Value}";

                        List.Add(nuget);
                    }
                }
            }
        }

        public ICommand ShowTestsPageCmd { get; }

        public ICommand DisplaySettingsPage { get; }

        public string AppName => AppInfo.Name;


        public string PackageName => AppInfo.PackageName;


        public string Version => AppInfo.VersionString;


        private string Build => AppInfo.BuildString;

        public IList<string> List { get; }

        public ICommand TapCommand => new Command<string>(url => Launcher.OpenAsync(new Uri(url)));
    }
}