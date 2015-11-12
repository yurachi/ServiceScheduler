using NUnit.Framework;
using ServiceScheduler;
using ServiceSchedulerTests.mocks;
using ServiceSchedulerTests.wrappers;
using System;

namespace ServiceSchedulerTests.tests.ServiceCallerTests
{
    [TestFixture]
    public class MainLoopTest
    {
        [Test]
        public void CurrentTimeMatchingScheduledActionCalled()
        {
            var currentDateTime = new DateTime(2015, 9, 14, 9, 34, 01);
            var configFactory = new ConfigProviderMockFactory();
            var actualDoWorkCalled = false;

            configFactory.SetNextExecutionTimeReturn(
                new ExecutionDateTime()
                {
                    ScheduledTime = currentDateTime,
                });

            var objectUnderTest = new ServiceCallerWrapper(configFactory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop(() => { actualDoWorkCalled = true; });

            Assert.IsTrue(actualDoWorkCalled);
        }

        [Test]
        public void CurrentTimeMatchingScheduledOnceTimeRemoveCalled()
        {
            var currentDateTime = new DateTime(2015, 9, 14, 9, 34, 01);
            var configFactory = new ConfigProviderMockFactory();
            var actualRemoveCalled = false;

            configFactory.SetNextExecutionTimeReturn(
                new ExecutionDateTime()
                {
                    IsOnce = true,
                    ScheduledTime = currentDateTime,
                    Remove = () => { actualRemoveCalled = true; }
                });

            var objectUnderTest = new ServiceCallerWrapper(configFactory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop(() => { });

            Assert.IsTrue(actualRemoveCalled);
        }
    }
}
