﻿using Autofac;
using Doods.Framework.Std;
using Doods.Xam.MonitorMyServer.UWP.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
namespace Doods.Xam.MonitorMyServer.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Xamarin.Forms.Forms.SetFlags("Shell_UWP_Experimental");
            InitializeComponent();
            Suspending += OnSuspending;
            MonitorMyServer.App.SetupContainer(Bootstrapper.CreateContainer());
            LoadConfiguration().Wait();
            LoadSettings();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                Rg.Plugins.Popup.Popup.Init();
                FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
                Xamarin.Forms.Forms.Init(e, Rg.Plugins.Popup.Popup.GetExtraAssemblies());
                
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null) rootFrame.Navigate(typeof(MainPage), e.Arguments);

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }


        private async Task LoadConfiguration()
        {
            await LoadDoodsConfiguration();
        }
        //On<UWP>().SetImageSearchDirectory("Assets");
        private async Task LoadDoodsConfiguration()
        {
            var fileName = @"Assets\App.config";

#if DEBUG
            fileName = @"Assets\Debug.config";
#endif
            var installationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var sFile = await installationFolder.GetFileAsync(fileName);
            using (var stream = await sFile.OpenStreamForReadAsync())
            {
                using (var reader = XmlReader.Create(stream))
                {
                    var configService = MonitorMyServer.App.Container.Resolve<IConfiguration>();
                    configService.LoadConfiguration(reader);
                }
            }
        }

        private void LoadSettings()
        {
            //App.Container.Resolve<ISettings>().Initialize();
        }
    }
}