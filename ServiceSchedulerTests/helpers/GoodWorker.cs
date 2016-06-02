using ServiceScheduler.interfaces;
using System;

namespace ServiceSchedulerTests.helpers
{
    class GoodWorker : IAmGoodWorker
    {
        public Action<Exception> testHandle { get; set; }
        public Action testInvokeDoWork { get; set; }
        public Action testStop { get; set; }

        public void HandleException(Exception ex)
        {
            if (testHandle != null)
                testHandle(ex);
        }

        public void InvokeDoWork()
        {
            if(testInvokeDoWork != null)
                testInvokeDoWork();
        }

        public void Stop()
        {
            if(testStop != null)
                testStop();
        }
    }
}
