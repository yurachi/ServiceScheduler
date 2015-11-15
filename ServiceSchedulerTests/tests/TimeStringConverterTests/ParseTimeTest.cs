using System;
using NUnit.Framework;

namespace ServiceSchedulerTests
{
    [TestFixture]
	public class ParseTimeTest
	{
        [TestCase(2015, 9, 21, 1, 1, "01:01", "Monday")] //day before now; scheduling for the next week
        [TestCase(2015, 9, 15, 2, 3, "02:03", "Tuesday")] //time before now, but the day matches now; return the specified time
        [TestCase(2015, 9, 15, 9, 34, "09:34", "Tuesday")] // time and day matches now; return the specified time
        [TestCase(2015, 9, 15, 19, 34, "19:34", "Tuesday")] 
        [TestCase(2015, 9, 16, 19, 34, "19:34", "Wednesday")]
        public void ParseValidTimeWithDayOfWeek(int expectedYear, int expectedMonth, int expectedDay, int expectedHour, int expectedMinute, string dateTime, string dayOfWeek)
        {
            var now = new DateTime(2015, 9, 15, 9, 34, 59);
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = now,
            };
            
            var expected = new DateTime(expectedYear,expectedMonth,expectedDay,expectedHour,expectedMinute, 0);

            var actual = objectUnderTest.ParseTime(dateTime, dayOfWeek);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(2015, 9, 16, 1, 1, "01:01", null)] //time before now; scheduling for the next day
        [TestCase(2015, 9, 15, 9, 34, "09:34", null)] // time matches now; return the specified time
        [TestCase(2015, 9, 15, 19, 34, "19:34", null)]
        public void ParseValidTimeWithoutDayOfWeek(int expectedYear, int expectedMonth, int expectedDay, int expectedHour, int expectedMinute, string dateTime, string dayOfWeek)
        {
            var now = new DateTime(2015, 9, 15, 9, 34, 59);
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = now,
            };

            var expected = new DateTime(expectedYear, expectedMonth, expectedDay, expectedHour, expectedMinute, 0);

            var actual = objectUnderTest.ParseTime(dateTime, dayOfWeek);

            Assert.AreEqual(expected, actual);
        }
    }
}

 