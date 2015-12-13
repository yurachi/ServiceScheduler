using NUnit.Framework;
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

        [Test]
        public void Insert3Times()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter)
            {
                Now = new System.DateTime(2015, 09, 14, 09, 30, 01),
            };
            var methodName = "Insert3Times";
            var expected = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 14, 09, 33, 01),
                ServiceMethodName = methodName,
            };
            var expectedLess1min = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 14, 09, 32, 01),
                ServiceMethodName = methodName,
            };

            var expectedPlus24h = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 15, 09, 34, 01),
                ServiceMethodName = methodName,
            };

            objectUnderTest.InsertExecutionTimes(new[] { expectedPlus24h, expectedLess1min, expected});

            var actual = objectUnderTest.ExecutionTimes.ElementAt(1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Insert2IdenticalTimes()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter);
            var expected = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ServiceMethodName = "Insert2IdenticalTimes",
            };
            var unexpected = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ServiceMethodName = "Insert2IdenticalTimes"
            };

            objectUnderTest.InsertExecutionTimes(new[] { expected, unexpected });

            var actual = objectUnderTest.ExecutionTimes.Where(x => x.ServiceMethodName == expected.ServiceMethodName).Single();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Insert2SameTimesOneStopper()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter);
            var expected = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ServiceMethodName = "Insert2IdenticalTimes",
            };
            var expectedWithStop = new ExecutionDateTime()
            {
                ScheduledTime = new System.DateTime(2015, 09, 14, 09, 34, 01),
                ServiceMethodName = "Insert2IdenticalTimes",
                IsStop = true,
            };

            objectUnderTest.InsertExecutionTimes(new[] { expected, expectedWithStop });

            var actual = objectUnderTest.ExecutionTimes.Where(x => x.ServiceMethodName == expected.ServiceMethodName).Single();

            Assert.AreEqual(expectedWithStop, actual, "Expected time with Stop flag missing");
        }
    }
}
