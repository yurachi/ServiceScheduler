namespace ServiceScheduler.interfaces
{
    public interface IMainLoopProvider
    {
        bool MainLoopRunning  { get; set;  }
        void InvokeMainLoop();
        void JoinMainLoop();
    }
}
