using ServiceScheduler;
using System;

namespace ServiceSchedulerTests.wrappers
{
    public class ServiceCallerWrapper : ServiceCaller
    {
        public ServiceCallerWrapper(IConfigProvider configProvider) : base (configProvider)
        {
        }

        new public TimeSpan CalculateAbsoluteTimeDifference(DateTime scheduledTime, DateTime dateTimeNow)
        {
            return base.CalculateAbsoluteTimeDifference(scheduledTime, dateTimeNow);
        }
    }
}
