using System;
using NUnit.Framework;

namespace ServiceSchedulerTests
{
    [TestFixture]
	public class ConvertTest
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
            objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, 15, 9, 24, 31),
                Tolerance = new TimeSpan(0, 1, 0),
            };


            var actual = objectUnderTest.Convert (time);

            helpers.Assert.ExecutionDateTimeMatch(expectedHour, expectedMinute, expectedDay,expectedMonth, expectedYear, expectedNow, expectedOnce, actual);
		}

		[TestCase (1, 1, 16, 9, 2015, false, false, "01:01")]
		[TestCase (1, 1, 16, 9, 2015, false, true, "01:01 once")]
		[TestCase (1, 1, 1, 1, 2015, false, false, "01:01 01-Jan")] 
		[TestCase (1, 1, 15, 10, 2016, false, false, "01:01 15-Oct-2016")]
		[TestCase (9, 24, 16, 9, 2015, true, false, "now")]
		[TestCase (9, 24, 16, 9, 2015, true, true, "now once")]
		public void ConvertValidTomorrowTime (int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, bool expectedNow, bool expectedOnce, string time)
		{
            objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, 15, 9, 24, 31),
                Tolerance = new TimeSpan(0, 1, 0),
            };

            var actual = objectUnderTest.Convert (time);

            helpers.Assert.ExecutionDateTimeMatch(expectedHour, expectedMinute, expectedDay,expectedMonth, expectedYear, expectedNow, expectedOnce, actual);
		}

        [TestCase (1, 1, 15, 9, 2015, false, false, "01:01")]
		[TestCase (9, 25, 15, 9, 2015, true, false, "now")]
		[TestCase (9, 25, 15, 9, 2015, true, true, "now once")]
		public void ConversionToleranceValidTodayTime (int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, bool expectedNow, bool expectedOnce, string time)
		{
            objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, 15, 9, 24, 31),
                Tolerance = new TimeSpan(0, 1, 0),
            };

            var actual = objectUnderTest.Convert (time);

            helpers.Assert.ExecutionDateTimeMatch(expectedHour, expectedMinute, expectedDay,expectedMonth, expectedYear, expectedNow, expectedOnce, actual);
		}

		[TestCase (1, 1, 15, 9, 2015, false, false, "01:61")]
		[TestCase (1, 1, 15, 9, 2015, false, false, "24:01 once")]
		[TestCase (1, 1, 15, 9, 2015, false, false, "01:01 31-Sep")] 
		[TestCase (1, 1, 15, 9, 2015, false, false, "01:01 01:02")]
		[TestCase (1, 1, 15, 9, 2015, false, false, "01:01 now")]
		[TestCase (1, 1, 15, 9, 2015, false, false, "new")]
		[TestCase (1, 1, 15, 9, 2015, false, false, "now one")]
		public void ConvertInvalidTime (int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, bool expectedNow, bool expectedOnce, string time)
		{
            objectUnderTest = new TimeStringConverterWrapper()
            {
                Now = new DateTime(2015, 9, 15, 9, 24, 31),
                Tolerance = new TimeSpan(0,1,0),
            };

			var actual = objectUnderTest.Convert (time);

            helpers.Assert.ExecutionDateTimeMatch(expectedHour, expectedMinute, expectedDay, expectedMonth, expectedYear, expectedNow, expectedOnce, actual);
		}

	}
}

