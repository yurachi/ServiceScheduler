using NSubstitute;
using ServiceScheduler;

namespace ServiceSchedulerTests.mocks
{
    class TimeStringConverterMockFactory
    {
        private ITimeStringConverter _timeStringConverterMock;

        public TimeStringConverterMockFactory()
        {
            _timeStringConverterMock = Substitute.For<ITimeStringConverter>();
        }

        public ITimeStringConverter Create()
        {
            return _timeStringConverterMock;
        }
    }
}
