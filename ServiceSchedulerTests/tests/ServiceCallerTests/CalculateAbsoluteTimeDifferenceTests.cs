using NUnit.Framework;
using ServiceScheduler.providers;
using ServiceSchedulerTests.mocks;
using ServiceSchedulerTests.wrappers;
using System;
using System.Globalization;

namespace ServiceSchedulerTests.tests.ServiceCallerTests
{
    [TestFixture]
    public class CalculateAbsoluteTimeDifferenceTests
    {
        [TestCase("01:02:00", "02:03 09-Sep-2015", "01:01 09-Sep-2015")]
        [TestCase ("03:04:00", "20:55 09-Sep-2015", "23:59 09-Sep-2015")]
        [TestCase("02:03:00", "00:02 09-Sep-2015", "21:59 08-Sep-2015")]
        public void CheckTimeDifference(string expected, string scheduled, string now)
        {
            var scheduledDateTime = DateTime.Parse(scheduled, CultureInfo.CreateSpecificCulture("en-UK"), DateTimeStyles.NoCurrentDateDefault);
            var nowTime = DateTime.Parse(now, CultureInfo.CreateSpecificCulture("en-UK"), DateTimeStyles.NoCurrentDateDefault);

            var actual = UtilityProvider.CalculateAbsoluteTimeDifference(scheduledDateTime, nowTime);

            Assert.AreEqual(expected, actual.ToString());
        }
    }
}
