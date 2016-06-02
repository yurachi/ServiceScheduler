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
        [TestCase(true, "2015-Sep-15 09:33:02", "2015-Sep-15 09:34")] //59 seconds tolerance
        [TestCase(true, "2015-Sep-15 09:34:58", "2015-Sep-15 09:34")] //59 seconds tolerance
        [TestCase(false, "2015-Sep-15 09:33:00", "2015-Sep-15 09:34")]
        [TestCase(false, "2015-Sep-15 09:35:00", "2015-Sep-15 09:34")]
        [TestCase(false, "2015-Sep-14 09:34", "2015-Sep-15 09:34")] //date mismatch
        public void WhenTimeMatchingScheduledActionCalled(bool expectedDoWorkCalled, string current, string scheduled)
        {
            var currentDateTime = DateTime.Parse(current);
            var scheduledDateTime = DateTime.Parse(scheduled);
            var configFactory = new ConfigProviderMockFactory();
            var workerFactory = new WorkerProviderMockFactory();
            var workerName = "TestWorker";
            var actualDoWorkCalled = false;

            configFactory.SetNextExecutionTimeReturn(
                new ExecutionDateTime()
                {
                    ScheduledTime = scheduledDateTime,
                    ServiceClassName = workerName,
                });

            workerFactory.AddWorker(workerName, () => { actualDoWorkCalled = true; }, () => { }, (ex) => { });

            var objectUnderTest = new MainLoopProviderWrapper(configFactory.Create(), workerFactory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop();

            Assert.AreEqual(expectedDoWorkCalled, actualDoWorkCalled);
        }

        [TestCase(true, true, "2015-Sep-15 09:34", "2015-Sep-15 09:34")]
        [TestCase(false, false, "2015-Sep-15 09:34", "2015-Sep-15 09:34")]
        [TestCase(false, true, "2015-Sep-15 09:34", "2015-Sep-15 09:35")]
        [TestCase(false, true,  "2015-Sep-15 09:34", "2015-Sep-15 09:33")]
        [TestCase(false, true, "2015-Sep-15 09:34", "2015-Sep-16 09:34")]
        public void WhenTimeMatchingScheduledOnceTimeRemoveCalled(bool expectedRemoveCalled, bool isOnce, string current, string scheduled)
        {
            var currentDateTime = DateTime.Parse(current);
            var scheduledDateTime = DateTime.Parse(scheduled);
            var configFactory = new ConfigProviderMockFactory();
            var workerFactory = new WorkerProviderMockFactory();
            var workerName = "TestWorker";
            var actualRemoveCalled = false;

            configFactory.SetNextExecutionTimeReturn(
                new ExecutionDateTime()
                {
                    IsOnce = isOnce,
                    ScheduledTime = scheduledDateTime,
                    Remove = () => { actualRemoveCalled = true; },
                });

            workerFactory.AddWorker(workerName, () => { }, () => { }, (ex) => { });

            var objectUnderTest = new MainLoopProviderWrapper(configFactory.Create(), workerFactory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop();

            Assert.AreEqual(expectedRemoveCalled, actualRemoveCalled);
        }

        [TestCase(true, "2015-Sep-15 09:34", "2015-Sep-15 09:34")]
        [TestCase(false, "2015-Sep-15 09:34", "2015-Sep-15 09:34")]
        public void WhenTimeMatchingScheduledActionThrowsHandleCalled(bool expectedHandleCalled, string current, string scheduled)
        {
            var currentDateTime = DateTime.Parse(current);
            var scheduledDateTime = DateTime.Parse(scheduled);
            var configFactory = new ConfigProviderMockFactory();
            var workerFactory = new WorkerProviderMockFactory();
            var workerName = "TestWorker";
            var actualHandleCalled = false;

            configFactory.SetNextExecutionTimeReturn(
                new ExecutionDateTime()
                {
                    ScheduledTime = scheduledDateTime,
                    ServiceClassName = workerName,
                });

            workerFactory.AddWorker(workerName, () => { if (expectedHandleCalled) throw new Exception(); }, () => { }, (ex) => { actualHandleCalled = true; });

            var objectUnderTest = new MainLoopProviderWrapper(configFactory.Create(), workerFactory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop();

            Assert.AreEqual(expectedHandleCalled, actualHandleCalled);
        }

        [Test]
        public void AfterMainLoopStopCalled()
        {
            
            var currentDateTime = DateTime.Parse("2015-Sep-15 09:34");
            var scheduledDateTime = DateTime.Parse("2015-Sep-15 09:34");
            var configFactory = new ConfigProviderMockFactory();
            var workerFactory = new WorkerProviderMockFactory();
            var workerName = "TestWorker";
            var expectedStopCalled = true;
            var actualStopCalled = false;

            configFactory.SetNextExecutionTimeReturn(
                new ExecutionDateTime()
                {
                    ScheduledTime = scheduledDateTime,
                    ServiceClassName = workerName,
                });

            workerFactory.AddWorker(workerName, () => { }, () => { actualStopCalled = true; }, (ex) => { });

            var objectUnderTest = new MainLoopProviderWrapper(configFactory.Create(), workerFactory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop();

            Assert.AreEqual(expectedStopCalled, actualStopCalled);
        }
    }
}
