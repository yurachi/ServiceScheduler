﻿using System;
using System.Linq;
using NUnit.Framework;
using ServiceSchedulerTests.mocks;

namespace ServiceSchedulerTests.tests.ConfigProviderTests
{
    [TestFixture]
    public class CreateResetConfigTimesTests
    {
        [Test]
        public void CheckAllConfigResetTimes()
        {
            var dataProvider = new DataProviderMockFactory().Create();
            var timeStringConverter = new TimeStringConverterMockFactory().Create();
            var objectUnderTest = new ConfigProviderWrapper(dataProvider, timeStringConverter)
            {
                Now = new DateTime(2015, 9, 15, 9, 34, 0),
            };
            var actual = objectUnderTest.CreateResetConfigTimes();
            Assert.AreEqual(177, actual.Count());
        }
    }
}