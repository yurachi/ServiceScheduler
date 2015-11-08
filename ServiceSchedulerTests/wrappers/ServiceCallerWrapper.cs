using ServiceScheduler;
using System;

namespace ServiceSchedulerTests.wrappers
{
    public class ServiceCallerWrapper : ServiceCaller
    {
        public ServiceCallerWrapper(IConfigProvider configProvider) : base (configProvider)
        {
        }

        new public TimeSpan CalculateAbsoluteTimeDifference(DateTime scheduledTime)
        {
            return base.CalculateAbsoluteTimeDifference(scheduledTime);
        }

        public DateTime Now
        {
            set
            {
                _dateTimeNow = () => value;
            }
        }
    }
}
