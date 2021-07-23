// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="ShellNavigationService.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2019
//  </copyright>
//  History:
//   2019/07/27 at 14:29:  aka therv.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Servicies
{
    public enum TelemetryEventType
    {
        None = 0,
        Application = 1,
        Navigation = 2,
        Fonction = 3,
        Synchronisation = 4,
        Failure = 5,
        BackgroundTask = 6,
        RefreshToken
    }

    public static class ITelemetryServiceExtensions
    {
        public static void Event(this ITelemetryService telemetry, TelemetryEventType type, string message,
            Dictionary<string, string> properties = null,
            Dictionary<string, double> measures = null)
        {
            telemetry.Event(TelemetryEventHelper.GetEvent(type, message), properties, measures);
        }

        public static void Metric(this ITelemetryService telemetry, TelemetryEventType type, string message,
            double value, Dictionary<string, string> properties = null)
        {
            telemetry.Metric(TelemetryEventHelper.GetEvent(type, message), value, properties);
        }
    }

    /// <summary>
    ///     Helper pour formattage chaine envoyé à la télémétrie
    /// </summary>
    internal class TelemetryEventHelper
    {
        internal static string GetEvent(TelemetryEventType type, string message)
        {
            return $"{type}: {message}";
        }
    }

    public class ShellNavigationService : NavigationBaseService, INavigationService
    {
        public ShellNavigationService(ILogger logger, ITelemetryService telemetryService) : base(logger,
            telemetryService)
        {
        }

        public override void Configure(string pageKey, Type pageType)
        {
            base.Configure(pageKey, pageType);
            Routing.RegisterRoute(pageKey, pageType);
        }

        public Task GoBack()
        {
            _telemetry.Event(TelemetryEventType.Navigation, "Go back");
            return Shell.Current.Navigation.PopAsync();
        }

        public Task NavigateModalAsync(string pageKey, bool animated = true)
        {
            throw new NotImplementedException();
        }

        public Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            throw new NotImplementedException();
        }

        public async Task NavigateAsync(string pageKey, bool animated = true)
        {
            _telemetry.Event(TelemetryEventType.Navigation, $"Go to pagekey {pageKey}");
            //var state = Shell.Current.CurrentState;
            //await Shell.Current.GoToAsync($"{state.Location}/{pageKey}", animated);
            await Shell.Current.GoToAsync($"{pageKey}", false);
        }

        public async Task NavigateAsync(string pageKey, object parameter, bool animated = true)
        {
            if (parameter is IQueryShellNavigationObject shellNavigationObject)
            {
                _telemetry.Event(TelemetryEventType.Navigation, $"Go to pagekey {pageKey}");
                //var state = Shell.Current.CurrentState;
                //await Shell.Current.GoToAsync($"{state.Location}/{pageKey}?{shellNavigationObject.ToQuery()}", animated);
                await Shell.Current.GoToAsync($"{pageKey}?{shellNavigationObject.ToQuery()}", false);
            }
            else
            {
                throw new InvalidOperationException($"You need pass a {nameof(IQueryShellNavigationObject)}");
            }
        }

        public Task GoToRootAsync()
        {
            _telemetry.Event(TelemetryEventType.Navigation, "Go to root");
            return Shell.Current.Navigation.PopToRootAsync();
        }
    }
}