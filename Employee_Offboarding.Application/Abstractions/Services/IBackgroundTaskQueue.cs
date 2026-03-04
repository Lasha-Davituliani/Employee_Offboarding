namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem);
        ValueTask<Func<CancellationToken, ValueTask>> DequeueBeckgroundWorkItemAsync(CancellationToken ct);
    }
}
