namespace ServiceScheduler.interfaces
{
    public interface IWorkerProvider
    {
        IAmGoodWorker GetWorker(string workerClassName);
    }
}
