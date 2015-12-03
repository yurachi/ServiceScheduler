using NUnit.Framework;
using System.Collections.Generic;
using ServiceScheduler;
using ServiceSchedulerTests.mocks;
using System.Linq;

namespace ServiceSchedulerTests.tests.ConfigProviderTests
{
    [TestFixture]
    public class RemoveServiceExecutionTimesTests
    {
        [Test]
        public void CheckResetConfigExecutionTimesStays()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var serviceExecutionDateTime = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 15, 09, 29, 01),
                ServiceMethodName = "ServiceProvider.ResetConfigExecutionTimes",
            };
            var expected = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 15, 09, 29, 01),
                ServiceMethodName = "ConfigProvider.ResetConfigExecutionTimes",
            };
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter)
            {
                Now = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ExecutionTimes = new List<ExecutionDateTime>(new[] { serviceExecutionDateTime, expected }),
            };

            objectUnderTest.RemoveServiceExecutionTimes();

            var actual = objectUnderTest.ExecutionTimes.Single();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CheckResetServiceExecutionTimesStays()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var serviceProviderExecutionDateTime = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 15, 09, 29, 01),
                ServiceMethodName = "ServiceProvider.ResetServiceExecutionTimes",
            };
            var serviceExecutionDateTime = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 15, 09, 29, 01),
                ServiceMethodName = "Service.WorkerMethod",
            };
            var expected = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 15, 09, 29, 01),
                ServiceMethodName = "ConfigProvider.ResetServiceExecutionTimes",
            };
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter)
            {
                Now = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ExecutionTimes = new List<ExecutionDateTime>(new[] { serviceProviderExecutionDateTime, serviceExecutionDateTime, expected }),
            };

            objectUnderTest.RemoveServiceExecutionTimes();

            var actual = objectUnderTest.ExecutionTimes.Single();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CheckServiceExecutionTimesGoes()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var serviceExecutionDateTime = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 15, 09, 29, 01),
                ServiceMethodName = "ServiceProvider.ResetServiceExecutionTimes",
            };
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter)
            {
                Now = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ExecutionTimes = new List<ExecutionDateTime>(new[] { serviceExecutionDateTime }),
            };

            objectUnderTest.RemoveServiceExecutionTimes();

            var actual = objectUnderTest.ExecutionTimes.Count;

            Assert.AreEqual(0, actual);
        }
    }
}
