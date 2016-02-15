using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceScheduler.providers
{
    public static class UtilityProvider
    {
        public static TimeSpan CalculateAbsoluteTimeDifference(DateTime scheduledTime, DateTime referenceTime)
        {
            if (scheduledTime > referenceTime)
                return scheduledTime.Subtract(referenceTime);
            else
                return referenceTime.Subtract(scheduledTime);
        }
    }
}
