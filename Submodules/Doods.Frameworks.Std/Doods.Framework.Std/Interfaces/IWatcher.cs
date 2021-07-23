using System;
using System.Collections.Generic;

namespace Doods.Framework.Std
{
    public interface IWatcherDescriptor
    {
        string Name { get; }

        string Key { get; }

        bool Telemetry { get; }

        bool Debug { get; }

        Dictionary<string, string> Properties { get; }

        Dictionary<string, double> Measures { get; }
    }

    public interface IWatcher : IWatcherDescriptor, IDisposable
    {
    }
}