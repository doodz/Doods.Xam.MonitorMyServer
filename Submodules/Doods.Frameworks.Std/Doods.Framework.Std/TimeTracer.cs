using System;
using System.Linq;
using Doods.Framework.Std.Extensions;

namespace Doods.Framework.Std
{
    public class TimeTracer : IDisposable
    {
        public enum TimeTracerContext
        {
            LogInRealTime,
            LogInDispose
        }

        private readonly ILogger _logger;
        private readonly Action<ILogger, string> _logging;
        private readonly ITelemetryService _telemetryService;

        public TimeTracer(ILogger logger, ITelemetryService telemetryService, TimeTracerContext? context = null,
            Action<ILogger, string> logging = null)
        {
            _logger = logger;
            _telemetryService = telemetryService;
            _logging = logging ?? ((l, s) => l.Info(s));

            Context = context ?? TimeTracerContext.LogInDispose;
            Timer = new TimeWatcher(context == TimeTracerContext.LogInRealTime ? this : null);

            IsActive = true;
        }

        public ITimeWatcher Timer { get; }

        public TimeTracerContext Context { get; }

        public bool IsActive { get; private set; }

        public void Dispose()
        {
            if (IsActive)
            {
                if (Context == TimeTracerContext.LogInDispose) _logging(_logger, $"{Timer.Traces.Join("\n")}");

                foreach (var watcher in Timer.Watchers.Where(w => w.Telemetry))
                    _telemetryService?.Event(watcher.Name, watcher.Properties, watcher.Measures);
            }

            IsActive = false;
        }

        public void Log(string msg)
        {
            _logging(_logger, msg);
        }
    }
}