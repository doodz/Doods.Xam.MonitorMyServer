using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace Doods.Xam.MonitorMyServer.Services
{
    public enum PidStatusCheckerState
    {
        Running,
        Cancelled,
        Finished
    }


    public class PidStatusChecker
    {
        private readonly Func<bool, Task> _callbackAction;
        private readonly CancellationToken _cancelToken;
        private readonly int _interval;

        //private ISshService ssh = new Lazy<ISshService>(s=> App.Container.Resolve<ILogger>());
        private readonly Lazy<ISshService> _lazySsh = new Lazy<ISshService>(() => App.Container.Resolve<ISshService>());
        private readonly int _pid;
        private readonly Timer _stateTimer;

        private readonly int maxCount = 20;

        private int _invokeCount;

        public bool IsRunning = true;

        public PidStatusCheckerState State = PidStatusCheckerState.Running;

        public PidStatusChecker(int pid, Func<bool, Task> callbackAction, CancellationToken cancelToken,
            int interval = 5000)
        {
            _cancelToken = cancelToken;
            _callbackAction = callbackAction;
            _invokeCount = 0;
            _pid = pid;
            _interval = interval;
            // Create an AutoResetEvent to signal the timeout threshold in the
            // timer callback has been reached.
            var autoEvent = new AutoResetEvent(false);
            _stateTimer = new Timer(CheckStatus, autoEvent, interval, interval);
        }


        public async void CheckStatus(object stateInfo)
        {
            var autoEvent = (AutoResetEvent) stateInfo;
            if (_cancelToken.IsCancellationRequested)
            {
                State = PidStatusCheckerState.Cancelled;
                _stateTimer.Dispose();
                //autoEvent.Set();
                await _callbackAction(IsRunning).ConfigureAwait(false);
                return;
            }

            if (_invokeCount++ != maxCount)
            {
                IsRunning = await _lazySsh.Value.IsRunning(_pid);
                if (!IsRunning)
                {
                    State = PidStatusCheckerState.Finished;
                    _stateTimer.Dispose();
                    //autoEvent.Set();
                    await _callbackAction(IsRunning).ConfigureAwait(false);
                }
            }
            else
            {
                State = PidStatusCheckerState.Finished;
                _stateTimer.Dispose();
                //autoEvent.Set();
                await _callbackAction(IsRunning).ConfigureAwait(false);
            }
        }
    }
}