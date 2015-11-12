using System;
using NSubstitute;
using ServiceScheduler;

namespace ServiceSchedulerTests.mocks
{
    public class ConfigProviderMockFactory
    {
        private IConfigProvider _configProviderMock;
        private TimeSpan _minimalTimeInterval;

        public ConfigProviderMockFactory()
        {
            _configProviderMock = Substitute.For<IConfigProvider>();
            _minimalTimeInterval = new TimeSpan(0, 0, 59);
            _configProviderMock.GetMinimalTimeInterval().Returns(_minimalTimeInterval);
        }

        public void SetNextExecutionTimeReturn(ExecutionDateTime time)
        {
            _configProviderMock.GetNextExecutionTime().Returns(time);
        }

        public IConfigProvider Create()
        {
            return _configProviderMock;
        }
    }
}
