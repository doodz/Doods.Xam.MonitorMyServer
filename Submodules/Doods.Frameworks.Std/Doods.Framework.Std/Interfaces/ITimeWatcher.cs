using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doods.Framework.Std
{
    public interface ITimeWatcher
    {
        string[] Traces { get; }

        IReadOnlyCollection<IWatcherDescriptor> Watchers { get; }

        void Start();

        void Stop();

        IWatcher StartWatcher(string name, bool telemetry = true);

        Task<IWatcher> StartWatcherAsync(string name, bool telemetry = true);

        void StopWatcher(string key, Dictionary<string, string> properties, Dictionary<string, double> measures);
    }
}