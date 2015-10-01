using System;
using NUnit.Framework;
using ServiceScheduler;

namespace ServiceSchedulerTests
{
	[TestFixture]
	public class ParseDateTimeTest
	{
		private TimeStringConverterWrapper objectUnderTest;

		[TestCase (1, 1, 15, 9, 2015, false, false, "01:01")]
		[TestCase (1, 1, 15, 9, 2015, false, true, "01:01 once")]
		[TestCase (1, 1, 1, 1, 2015, false, false, "01:01 01-Jan")] 
		[TestCase (1, 1, 15, 10, 2016, false, false, "01:01 15-Oct-2016")]
		[TestCase (9, 24, 15, 9, 2015, true, false, "now")]
		[TestCase (9, 24, 15, 9, 2015, true, true, "now once")]
		public void ConvertValidTodayTime (int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, bool expectedNow, bool expectedOnce, string time)
		{
			objectUnderTest = new TimeStringConverterWrapper ();
			objectUnderTest.Now = new DateTime (2015, 9, 15, 9, 24, 31);

			var actual = objectUnderTest.Convert (time, 0, new TimeSpan());

            helpers.Assert.ExecutionDateTimeMatch(expectedHour, expectedMinute, expectedDay,expectedMonth, expectedYear, expectedNow, expectedOnce, actual);
		}

	}
}

