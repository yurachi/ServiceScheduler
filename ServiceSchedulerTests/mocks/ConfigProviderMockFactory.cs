using NSubstitute;
using ServiceScheduler;

namespace ServiceSchedulerTests.mocks
{
    public class ConfigProviderMockFactory
    {
        private IConfigProvider _configProviderMock;

        public ConfigProviderMockFactory()
        {
            _configProviderMock = Substitute.For<IConfigProvider>();
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
