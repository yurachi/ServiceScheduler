using NUnit.Framework;
using ServiceSchedulerTests.mocks;
using ServiceSchedulerTests.wrappers;
using System;
using System.Globalization;

namespace ServiceSchedulerTests.tests.ServiceCallerTests
{
    [TestFixture]
    public class CalculateAbsoluteTimeDifferenceTests
    {
        [TestCase ("01:01:00", "01:01 09-Sep-2015", "02:02 09-Sep-2015")]
        [TestCase("01:01:00", "02:02 09-Sep-2015", "01:01 09-Sep-2015")]
        public void CheckTimeDifference(string expected, string scheduled, string now)
        {
            var configProvider = new ConfigProviderMockFactory().Create();
            var objectUnderTest = new ServiceCallerWrapper(configProvider);
            var scheduledDateTime = DateTime.Parse(scheduled, CultureInfo.CreateSpecificCulture("en-UK"), DateTimeStyles.NoCurrentDateDefault);
            objectUnderTest.Now = DateTime.Parse(now, CultureInfo.CreateSpecificCulture("en-UK"), DateTimeStyles.NoCurrentDateDefault);

            var actual = objectUnderTest.CalculateAbsoluteTimeDifference(scheduledDateTime);

            Assert.AreEqual(expected, actual.ToString());
        }
    }
}
