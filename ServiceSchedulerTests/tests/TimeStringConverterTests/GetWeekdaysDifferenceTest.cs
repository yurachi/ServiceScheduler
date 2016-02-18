using NUnit.Framework;
using System;

namespace ServiceSchedulerTests.tests.TimeStringConverterTests
{
    [TestFixture]
    public class GetWeekDaysDifferenceTest
    {
        [TestCase(0, "Sunday", 13)]
        [TestCase(1, "Monday", 13)]
        [TestCase(2, "Tuesday", 13)]
        [TestCase(3, "Wednesday", 13)]
        [TestCase(4, "Thursday", 13)]
        [TestCase(5, "Friday", 13)]
        [TestCase(6, "Saturday", 13)]
        public void ScheduledDayOnOrAfterCurrentDay(int expected, string scheduled, int day)
        {
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, day, 9, 34, 0),
            };

            var actual = objectUnderTest.GetWeekDaysDifference(scheduled);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "Sunday", 19)]
        [TestCase(2, "Monday", 19)]
        [TestCase(3, "Tuesday", 19)]
        [TestCase(4, "Wednesday", 19)]
        [TestCase(5, "Thursday", 19)]
        [TestCase(6, "Friday", 19)]
        public void ScheduledDayBeforeCurrentWeekday(int expected, string scheduled, int day)
        {
            var objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, day, 9, 34, 0),
            };

            var actual = objectUnderTest.GetWeekDaysDifference(scheduled);

            Assert.AreEqual(expected, actual);
        }
    }
}
