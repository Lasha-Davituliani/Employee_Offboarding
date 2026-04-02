using Employee_Offboarding.Application.Abstractions.Services;
using System.Collections.Concurrent;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<Func<CancellationToken, ValueTask>> _workItems = new();
        private readonly SemaphoreSlim _signal = new(0);

        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueBeckgroundWorkItemAsync(CancellationToken ct)
        {
            await _signal.WaitAsync(ct);
            _workItems.TryDequeue(out var workItem);
            return workItem;
        }

        public ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem)
        {
            if(workItem == null)
                throw new ArgumentNullException(nameof(workItem));
            _workItems.Enqueue(workItem);
            _signal.Release();
            return ValueTask.CompletedTask;
        }
    }
}
