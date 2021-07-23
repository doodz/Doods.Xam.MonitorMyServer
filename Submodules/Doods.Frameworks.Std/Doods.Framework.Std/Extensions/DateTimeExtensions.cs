using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doods.Framework.Std.Extensions
{
    public static class TaskExtensions
    {
        // see https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md#always-dispose-cancellationtokensources-used-for-timeouts
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            using (var cts = new CancellationTokenSource())
            {
                var delayTask = Task.Delay(timeout, cts.Token);

                var resultTask = await Task.WhenAny(task, delayTask).ConfigureAwait(false);
                if (resultTask == delayTask)
                    // Operation cancelled
                    throw new OperationCanceledException();
                cts.Cancel();
            }

            await task.ConfigureAwait(false);
        }

        // see https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md#always-dispose-cancellationtokensources-used-for-timeouts
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            // This disposes the registration as soon as one of the tasks trigger
            using (cancellationToken.Register(state => { ((TaskCompletionSource<object>) state).TrySetResult(null); },
                tcs))
            {
                var resultTask = await Task.WhenAny(task, tcs.Task).ConfigureAwait(false);
                if (resultTask == tcs.Task)
                    // Operation cancelled
                    throw new OperationCanceledException(cancellationToken);

                return await task.ConfigureAwait(false);
            }
        }
    }

    public static class DateTimeExtensions
    {
        public static long ToUnixTimestamp(this DateTime d)
        {
            var epoch = d - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long) epoch.TotalSeconds;
        }
    }
}