using System;
using NUnit.Framework;

namespace ServiceSchedulerTests
{
    [TestFixture]
	public class ParseTimeTest
	{
        [TestCase(2015, 9, 21, 1, 1, "01:01", "Monday")]
        [TestCase(2015, 9, 22, 2, 3, "02:03", "Tuesday")]
        [TestCase(2015, 9, 15, 9, 34, "09:34", "Tuesday")]
        [TestCase(2015, 9, 15, 19, 34, "19:34", "Tuesday")]
        [TestCase(2015, 9, 16, 19, 34, "19:34", "Wednesday")]
        public void ParseValidTimeWithDayOfWeek(int expectedYear, int expectedMonth, int expectedDay, int expectedHour, int expectedMinute, string dateTime, string dayOfWeek)
        {
            var now = new DateTime(2015, 9, 15, 9, 34, 1);
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = now,
            };
            
            var expected = new DateTime(expectedYear,expectedMonth,expectedDay,expectedHour,expectedMinute, 0);

            var actual = objectUnderTest.ParseTime(dateTime, dayOfWeek);

            Assert.AreEqual(expected, actual);
        }
    }
}

 