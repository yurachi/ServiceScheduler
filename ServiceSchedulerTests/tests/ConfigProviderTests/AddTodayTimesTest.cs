using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ServiceSchedulerTests
{
	namespace ConfigProviderTests
	{
		[TestFixture]
		public class AddTodayTimesTest
		{
			private ConfigProviderWrapper objectUnderTest;

			[TestCase (1, "01:01")]
			[TestCase (1, "01:01 once")]
			[TestCase (0, "01:01 01-Jan")] //not today
			[TestCase (1, "01:01 15-Sep-2015")]
			[TestCase (1, "now")]
			[TestCase (1, "now once")]
			public void AddValidTime (int expectedExecutionTimesCount, params string[] times)
			{
				var dataProvider = new DataProviderMockFactory ()
					.Create ();
				
				objectUnderTest = new ConfigProviderWrapper (dataProvider);
				objectUnderTest.Now = new DateTime (2015, 9, 15);

				var recurrentTimeCollection = new List<string>();
				recurrentTimeCollection.AddRange (times);
					
				objectUnderTest.AddTodayTimes (recurrentTimeCollection);

				Assert.AreEqual(expectedExecutionTimesCount, objectUnderTest.ExecutionTimes.Count);
			}

			[TestCase (0, "01:61")]
			[TestCase (0, "24:01 once")]
			[TestCase (0, "01:01 31-Sep")] //not today
			[TestCase (0, "01:01 01:02")]
			[TestCase (0, "01:01 now")]
			[TestCase (0, "new")]
			[TestCase (0, "now one")]
			public void AddInvalidTime (int expectedExecutionTimesCount, params string[] times)
			{
				var dataProvider = new DataProviderMockFactory ()
					.Create ();

				objectUnderTest = new ConfigProviderWrapper (dataProvider);
				objectUnderTest.Now = new DateTime (2015, 9, 15);

				var recurrentTimeCollection = new List<string>();
				recurrentTimeCollection.AddRange (times);

				objectUnderTest.AddTodayTimes (recurrentTimeCollection);

				Assert.AreEqual(expectedExecutionTimesCount, objectUnderTest.ExecutionTimes.Count);
			}
		}
	}
}

