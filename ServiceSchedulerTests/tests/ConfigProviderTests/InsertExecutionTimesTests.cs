﻿using NUnit.Framework;
using ServiceScheduler;
using ServiceSchedulerTests.mocks;
using System.Linq;

namespace ServiceSchedulerTests.tests.ConfigProviderTests
{
    [TestFixture]
    class InsertExecutionTimesTests
    {
        [Test]
        public void InsertSingleTime()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter);
            var expected = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ServiceMethodName = "InsertSingleTime",
            };

            objectUnderTest.InsertExecutionTimes(new[] { expected });

            var actual = objectUnderTest.ExecutionTimes.Where(x => x.ServiceMethodName == expected.ServiceMethodName).Single();

            Assert.AreEqual(expected, actual);
        }
    }
}
