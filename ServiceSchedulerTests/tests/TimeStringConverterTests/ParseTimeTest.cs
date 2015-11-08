using System;
using NUnit.Framework;

namespace ServiceSchedulerTests
{
    [TestFixture]
	public class ParseTimeTest
	{
        [TestCase(1, 1, 15, 9, 2015, "01:01", null)]
        [TestCase(1, 1, 15, 9, 2015, "01:01", "Monday")]
        public void ParseValidTimeWithWeekDay(int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, string dateTime, string dayOfWeek)
        {
            var objectUnderTest = new TimeStringConverterWrapper();
            objectUnderTest.Now = new DateTime(expectedYear, expectedMonth, expectedDay, expectedHour, expectedMinute, 0);
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay, expectedHour, expectedMinute, 0);

            var actual = objectUnderTest.ParseTime(dateTime, dayOfWeek);

            Assert.AreEqual(expected, actual);
        }
    }
}

 