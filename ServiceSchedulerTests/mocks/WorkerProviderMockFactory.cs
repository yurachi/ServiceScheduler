using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using ServiceScheduler.interfaces;
using ServiceSchedulerTests.helpers;

namespace ServiceSchedulerTests.mocks
{
    class WorkerProviderMockFactory
    {
        private IWorkerProvider _workerProviderMock;

        public WorkerProviderMockFactory()
        {
            _workerProviderMock = Substitute.For<IWorkerProvider>();
        }

        public IWorkerProvider Create()
        {
            return _workerProviderMock;
        }

        internal void AddWorker(string workerName, Action doWork, Action stop, Action<Exception> handleException)
        {
            _workerProviderMock.GetWorker(workerName)
                .Returns(new GoodWorker()
                    {
                        testInvokeDoWork = doWork,
                        testStop = stop,
                        testHandle = handleException,
                    });
        }
    }
}
