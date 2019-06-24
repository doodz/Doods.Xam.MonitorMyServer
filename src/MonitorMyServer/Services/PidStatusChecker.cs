using System;
using System.Threading;
using Autofac;

namespace Doods.Xam.MonitorMyServer.Services
{
    public enum PidStatusCheckerState
    {

        Running,
        Cancelled,
        Finished,

    }


    public class PidStatusChecker
    {
        private readonly CancellationToken _cancelToken;
        private readonly int _interval;
        private readonly int _pid;
        private readonly Action<bool> _callbackAction;
        private readonly Timer _stateTimer;

        //private ISshService ssh = new Lazy<ISshService>(s=> App.Container.Resolve<ILogger>());
        private readonly Lazy<ISshService> _lazySsh = new Lazy<ISshService>(() => App.Container.Resolve<ISshService>());

        private int _invokeCount;

        public bool IsRunning = true;

        private readonly int maxCount = 20;

        public PidStatusCheckerState State = PidStatusCheckerState.Running;
        public PidStatusChecker(int pid,Action<bool> callbackAction, CancellationToken cancelToken, int interval = 5000)
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
            var autoEvent = (AutoResetEvent)stateInfo;
            if (_cancelToken.IsCancellationRequested)
            {
                State = PidStatusCheckerState.Cancelled;
                autoEvent.Set();
                return;
            }
            if (_invokeCount++ != maxCount)
            {
                IsRunning = await _lazySsh.Value.IsRunning(_pid);
                if (!IsRunning)
                {
                    _callbackAction(IsRunning);
                    State = PidStatusCheckerState.Finished;
                    autoEvent.Set();
                }

            }
            else
            {
                _callbackAction(IsRunning);
                State = PidStatusCheckerState.Finished;
                autoEvent.Set();
            }
        }
    }
}