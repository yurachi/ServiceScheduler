using ServiceScheduler;
using ServiceScheduler.interfaces;
using ServiceScheduler.providers;
using ServiceSchedulerTests.mocks;
using System;

namespace ServiceSchedulerTests.wrappers
{
    public class MainLoopProviderWrapper : MainLoopProvider
    {
        public MainLoopProviderWrapper(IConfigProvider configProvider, IWorkerProvider workerProvider) : base (configProvider, workerProvider)
        {
            var factory = new BoolPropertyMockFactory();
            factory.SetReturnValues(true, false);
            MainLoopRunningMock = factory.Create();
        }

        public DateTime Now
        {
            set
            {
                _dateTimeNow = () => value;
            }
        }

        public BoolPropertyMockFactory.IBoolProperty MainLoopRunningMock { get; set; }

        override public bool MainLoopRunning
        {
            get
            {
                return MainLoopRunningMock.Property;
            }
        }
    }
}
