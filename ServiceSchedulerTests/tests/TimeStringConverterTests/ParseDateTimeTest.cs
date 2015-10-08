using System;
using NUnit.Framework;
using ServiceScheduler;

namespace ServiceSchedulerTests
{
	[TestFixture]
	public class ParseDateTimeTest
	{
		private TimeStringConverterWrapper objectUnderTest;

		[TestCase (0, 0, 16, 9, 2015, "0:0")]
        [TestCase(8, 45, 16, 9, 2015, "08:45")]
        [TestCase(9, 23, 16, 9, 2015, "09:23")]
        [TestCase(9, 24, 15, 9, 2015, "09:24")]
        [TestCase(23, 59, 15, 9, 2015, "23:59")]
        public void ParseValidTime (int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, string dateTime)
		{
			objectUnderTest = new TimeStringConverterWrapper ();
			objectUnderTest.Now = new DateTime (2015, 9, 15, 9, 24, 0);
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay, expectedHour, expectedMinute, 0);

			var actual = objectUnderTest.ParseDateTime (dateTime);

            Assert.AreEqual(expected, actual);
		}

        [TestCase(1, 1, 1, 1, 2016, "01:01 01-Jan")]
        [TestCase(1, 1, 15, 10, 2017, "01:01 15-Oct-2017")]
        [TestCase(9, 10, 15, 8, 2016, "09:10 15-Aug")]
        public void ParseValidTimeWithDate(int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, string dateTime)
        {
            objectUnderTest = new TimeStringConverterWrapper();
            objectUnderTest.Now = new DateTime(2015, 9, 15, 9, 24, 0);
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay, expectedHour, expectedMinute, 0);

            var actual = objectUnderTest.ParseDateTime(dateTime);

            Assert.AreEqual(expected, actual);
        }
    }
}

