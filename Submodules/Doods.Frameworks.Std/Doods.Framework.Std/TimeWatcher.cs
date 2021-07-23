using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Doods.Framework.Std.Extensions;

namespace Doods.Framework.Std
{
    public class TimeWatcher : ITimeWatcher, IDisposable
    {
        private readonly Dictionary<string, TimeDescription> _cache;
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private readonly TimeTracer _timeTracer;
        private int _count;

        public TimeWatcher(TimeTracer timeTracer = null)
        {
            _timeTracer = timeTracer;
            _cache = new Dictionary<string, TimeDescription>();
            Start();
        }

        public void Dispose()
        {
            if (_lock.CurrentCount != 1)
                _lock.Release();
        }

        public void Start()
        {
            if (_cache.IsNotEmpty())
            {
                Stop();
                _cache.Clear();
            }
        }

        public void Stop()
        {
            if (_count > 0 && _cache.IsNotEmpty())
                foreach (var kvp in _cache)
                    kvp.Value.Watcher.Stop();
            _count = 0;
        }


        public IWatcher StartWatcher(string name, bool telemetry)
        {
            return StartWatcherInternal(name, telemetry, false);
        }

        public async Task<IWatcher> StartWatcherAsync(string name, bool telemetry)
        {
            using (this)
            {
                await _lock.WaitAsync();
                var watcher = StartWatcherInternal(name, telemetry, false);
                return watcher;
            }
        }

        public void StopWatcher(string key, Dictionary<string, string> properties, Dictionary<string, double> measures)
        {
            if (!_cache.ContainsKey(key)) return;

            var time = _cache[key];
            time.Watcher.Stop();

            properties = properties ?? new Dictionary<string, string>();
            measures = measures ?? new Dictionary<string, double>();
            measures.Add("ms", time.Watcher.ElapsedMilliseconds);

            var value = new TimeDescription(time.Name, time.Key, time.Telemetry, time.Debug, time.Watcher, time.Level,
                properties, measures);

            _timeTracer?.Log($"{value.FormatLevel()}{key.Split(':')[0]}: {value.Format()}");

            _cache[key] = value;
            _count--;
        }

        public string[] Traces => GetTraces();

        public IReadOnlyCollection<IWatcherDescriptor> Watchers => GetWatchers();

        private IReadOnlyCollection<IWatcherDescriptor> GetWatchers()
        {
            return _cache.Values.ToList();
        }

        private IWatcher StartWatcherInternal(string name, bool telemetry, bool debug)
        {
            var key = FormatKey(name, DateTime.Now.Ticks);
            if (_cache.ContainsKey(key)) return null;

            _cache.Add(key, new TimeDescription(name, key, telemetry, debug, Stopwatch.StartNew(), _count));
            _count++;

            return new Watcher(this, name, key, telemetry, debug);
        }

        private static string FormatKey(string name, long tick)
        {
            return $"{name}:{tick}";
        }

        private string[] GetTraces()
        {
            var rslt = _cache.Select(kvp => $"{kvp.Value.FormatLevel()}{kvp.Key.Split(':')[0]}: {kvp.Value.Format()}");
            return rslt.ToArray();
        }

        private class Watcher : IWatcher
        {
            private readonly ITimeWatcher _timeWatcher;

            public Watcher(ITimeWatcher timeWatcher, string name, string key, bool telemetry, bool debug)
            {
                _timeWatcher = timeWatcher;
                Name = name;
                Key = key;
                Telemetry = telemetry;
                Debug = debug;
                Properties = new Dictionary<string, string>();
                Measures = new Dictionary<string, double>();
            }

            public Dictionary<string, string> Properties { get; }

            public Dictionary<string, double> Measures { get; }

            public string Name { get; }

            public string Key { get; }

            public bool Telemetry { get; }

            public bool Debug { get; }


            public void Dispose()
            {
                _timeWatcher.StopWatcher(Key, Properties, Measures);
            }
        }

        private class TimeDescription : IWatcherDescriptor
        {
            public TimeDescription(string name, string key, bool telemetry, bool debug, Stopwatch watcher, int level,
                Dictionary<string, string> properties = null, Dictionary<string, double> measures = null)
            {
                Name = name;
                Key = key;
                Telemetry = telemetry;
                Debug = debug;
                Watcher = watcher;
                Level = level;
                Properties = properties;
                Measures = measures;
            }


            public Stopwatch Watcher { get; }

            public int Level { get; }

            public string Name { get; }

            public string Key { get; }

            public bool Telemetry { get; }

            public bool Debug { get; }

            public Dictionary<string, string> Properties { get; }

            public Dictionary<string, double> Measures { get; }

            public string Format()
            {
                var r = new List<string>(Properties?.Count ?? 0 + Measures?.Count ?? 0);

                if (Properties.IsNotEmpty()) r.AddRange(Properties.Select(d => $"{d.Key}={d.Value}"));

                if (Measures.IsNotEmpty()) r.AddRange(Measures.Select(d => $"{d.Key}={d.Value}"));

                return r.Join(", ");
            }

            public string FormatLevel()
            {
                var str = string.Empty;
                if (Level > 0)
                {
                    var i = 1;
                    do
                    {
                        str += "   ";
                    } while (i++ < Level);

                    str += "> ";
                }

                return str;
            }
        }
    }
}