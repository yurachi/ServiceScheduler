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
			[TestCase (0, "01:01 01-Jan")]
			public void AddTime (int expectedExecutionTimesCount, params string[] times)
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

