using System;
using NUnit.Framework;
using ServiceScheduler.datamodels;

namespace ServiceSchedulerTests
{
    [TestFixture]
	public class ConvertTest
	{
        [Test]
        public void ShouldReturnExecutionDateTime()
        {
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, 14, 9, 34, 0),
            };

            var sourceDateTime = new DataSourceDateTime()
            {
                DayOfWeek = "Monday",
                IsOnce = false,
                IsStop = false,
                ScheduledTime = "18:34",
                Callback = null
            };

            var actual = objectUnderTest.Convert(sourceDateTime);

            Assert.IsNotNull(actual);
        }
	}
}

