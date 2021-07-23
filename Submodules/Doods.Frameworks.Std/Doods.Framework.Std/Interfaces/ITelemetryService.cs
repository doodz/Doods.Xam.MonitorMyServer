using System;
using System.Collections.Generic;

namespace Doods.Framework.Std
{
    public interface ITelemetryService
    {
        bool IsActive { get; }

        void Event(string name, Dictionary<string, string> properties = null,
            Dictionary<string, double> measures = null);

        void Metric(string name, double value, Dictionary<string, string> properties = null);

        void Dependency(string type, string target, string name, string message, DateTimeOffset start,
            TimeSpan duration, string resultcode, bool success);

        void Exception(Exception exception, Dictionary<string, string> properties = null,
            Dictionary<string, double> measures = null);

        void Request(string name, DateTimeOffset start, TimeSpan duration, string responseCode, bool success);
    }
}