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
        [TestCase(true, "2015-Sep-15 09:34", "2015-Sep-15 09:34")]
        [TestCase(true, "2015-Sep-15 09:33:01", "2015-Sep-15 09:34")] //59 seconds tolerance
        [TestCase(true, "2015-Sep-15 09:34:59", "2015-Sep-15 09:34")] //59 seconds tolerance
        [TestCase(false, "2015-Sep-15 09:33:00", "2015-Sep-15 09:34")]
        [TestCase(false, "2015-Sep-15 09:35:00", "2015-Sep-15 09:34")]
        [TestCase(false, "2015-Sep-14 09:34", "2015-Sep-15 09:34")] //date mismatch
        public void WhenTimeMatchingScheduledActionCalled(bool expectedDoWorkCalled, string current, string scheduled)
        {
            var currentDateTime = DateTime.Parse(current);
            var scheduledDateTime = DateTime.Parse(scheduled);
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

            Assert.AreEqual(expectedDoWorkCalled, actualDoWorkCalled);
        }

        [TestCase(true, true, "2015-Sep-15 09:34", "2015-Sep-15 09:34")]
        [TestCase(false, false, "2015-Sep-15 09:34", "2015-Sep-15 09:34")]
        [TestCase(false, true, "2015-Sep-15 09:34", "2015-Sep-15 09:35")]
        [TestCase(false, true,  "2015-Sep-15 09:34", "2015-Sep-15 09:33")]
        [TestCase(false, true, "2015-Sep-15 09:34", "2015-Sep-16 09:34")]
        public void WhenTimeMatchingScheduledOnceTimeRemoveCalled(bool expectedRemoveCalled, bool isOnce, string current, string scheduled)
        {
            var currentDateTime = new DateTime(2015, 9, 14, 9, 34, 01);
            var configFactory = new ConfigProviderMockFactory();
            var actualRemoveCalled = false;

            configFactory.SetNextExecutionTimeReturn(
                new ExecutionDateTime()
                {
                    IsOnce = isOnce,
                    ScheduledTime = currentDateTime,
                    Remove = () => { actualRemoveCalled = true; }
                });

            var objectUnderTest = new ServiceCallerWrapper(configFactory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop(() => { });

            Assert.AreEqual(expectedRemoveCalled, actualRemoveCalled);
        }
    }
}
