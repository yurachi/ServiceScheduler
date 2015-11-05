using NUnit.Framework;
using System;

namespace ServiceSchedulerTests.tests.TimeStringConverterTests
{
    [TestFixture]
    public class GetDayForPastTimeTest
    {
        [TestCase(0, "23:59", null)]
        [TestCase(0, "09:34", null)]
        [TestCase(1, "09:33", null)]
        [TestCase(1, "00:00", null)]
        public void AddDayWithoutWeekDays(int expected, string parsedTime, string dayOfWeek)
        {
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, 14, 9, 34, 0),
            };
            var parsedDateTime = DateTime.Parse(parsedTime);

            var actual = objectUnderTest.GetDayForPastTime(parsedDateTime, dayOfWeek);

            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(0, "09:10", "Monday")]
        [TestCase(0, "09:20", "Tuesday")]
        [TestCase(0, "09:20", "Wednesday")]
        [TestCase(0, "08:00", "Thursday")]
        [TestCase(0, "08:00", "Friday")]
        [TestCase(0, "08:00", "Saturday")]
        [TestCase(0, "08:00", "Sunday")]
        public void AddDayWithWeekDay(int expected, string parsedTime, string dayOfWeek)
        {
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, 14, 9, 34, 0),
            };
            var parsedDateTime = DateTime.Parse(parsedTime);

            var actual = objectUnderTest.GetDayForPastTime(parsedDateTime, dayOfWeek);

            Assert.AreEqual(expected, actual);
        }
    }
}
