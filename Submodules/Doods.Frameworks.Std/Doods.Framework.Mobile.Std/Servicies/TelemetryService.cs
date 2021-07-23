using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Std;
using Doods.Framework.Std.Extensions;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Doods.Framework.Mobile.Std.Servicies
{
    internal class TelemetryService : ITelemetryService
    {
        public TelemetryService(IConfiguration config)
        {
            if (!string.IsNullOrEmpty(config.MobileCenterKey)) IsActive = true;
        }

        public bool IsActive { get; }

        public void Event(string name, Dictionary<string, string> properties = null,
            Dictionary<string, double> measures = null)
        {
            if (!IsActive) return;

            try
            {
                properties = UpdateProperties(properties, measures);

                Device.BeginInvokeOnMainThread(() => { Analytics.TrackEvent(name, properties); });
            }
            catch (Exception e)
            {
                Exception(e);
            }
        }

        public void Metric(string name, double value, Dictionary<string, string> properties = null)
        {
            if (!IsActive) return;
            throw new NotImplementedException();
        }

        public void Dependency(string type, string target, string name, string message, DateTimeOffset start,
            TimeSpan duration,
            string resultcode, bool success)
        {
            if (!IsActive) return;
            throw new NotImplementedException();
        }

        public void Exception(Exception exception, Dictionary<string, string> properties = null,
            Dictionary<string, double> measures = null)
        {
            if (!IsActive) return;

            properties = UpdateProperties(properties, measures);

            var attachments = new[]
            {
                ErrorAttachmentLog.AttachmentWithText(nameof(exception.Message), exception.Message)
            };

            Crashes.TrackError(exception, properties, attachments);
        }

        public void Request(string name, DateTimeOffset start, TimeSpan duration, string responseCode, bool success)
        {
            if (!IsActive) return;
            throw new NotImplementedException();
        }

        private Dictionary<string, string> UpdateProperties(Dictionary<string, string> properties,
            Dictionary<string, double> measures)
        {
            var final = new Dictionary<string, string>(5);

            if (properties == null) properties = new Dictionary<string, string>();
            var context = new StringBuilder();


            if (context.Length > 0) final.Add("context", context.ToString());

            if (properties.IsNotEmpty())
                foreach (var property in properties)
                    final.Add(property.Key, property.Value);

            if (measures.IsNotEmpty())
            {
                var m = new StringBuilder();
                foreach (var measure in measures) m.Append($"{measure.Key}={measure.Value};");

                final.Add("measures", m.ToString());
            }

            return final;
        }
    }
}