using ServiceScheduler;
using System;

namespace ServiceSchedulerTests.helpers
{
    public class Assert
    {
        public static void ExecutionDateTimeMatch(int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, bool expectedNow, bool expectedOnce, ExecutionDateTime actual)
        {
            throw new NotImplementedException();
            //TODO: throw AssertionException with the mismatched items
        }

    }
}
