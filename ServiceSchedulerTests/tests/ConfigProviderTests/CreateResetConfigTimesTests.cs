using System;
using System.Linq;
using NUnit.Framework;

namespace ServiceSchedulerTests.tests.ConfigProviderTests
{
    [TestFixture]
    public class CreateResetConfigTimesTests
    {
        [Test]
        public void CheckAllConfigResetTimes()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var objectUnderTest = new ConfigProviderWrapper(dataProvider)
            {
                Now = new DateTime(2015, 9, 15, 9, 34, 0),
            };
            var actual = objectUnderTest.CreateResetConfigTimes();
            Assert.AreEqual(177, actual.Count());
        }
    }
}
