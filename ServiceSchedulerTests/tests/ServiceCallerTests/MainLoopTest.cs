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
        public void OnceTimeMatchingCurrentIsRemoved()
        {
            var currentDateTime = new DateTime(2015, 9, 14, 9, 34, 01);
            var factory = new ConfigProviderMockFactory();
            var actualRemoveCalled = false;

            factory.SetNextExecutionTimeReturn(new ExecutionDateTime()
            {
                ScheduledTime = currentDateTime,
                Remove = () => { actualRemoveCalled = true; }
            });
            var objectUnderTest = new ServiceCallerWrapper(factory.Create())
            {
                Now = currentDateTime,
            };

            objectUnderTest.MainLoop(()=> { objectUnderTest.StopMainLoop = true; });

            Assert.IsTrue(actualRemoveCalled);
        }
    }
}
