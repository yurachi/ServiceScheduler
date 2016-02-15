using ServiceScheduler;
using ServiceSchedulerTests.mocks;
using System;

namespace ServiceSchedulerTests.wrappers
{
    public class ServiceCallerWrapper : ServiceCaller
    {
        public ServiceCallerWrapper(IConfigProvider configProvider) : base (configProvider)
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
