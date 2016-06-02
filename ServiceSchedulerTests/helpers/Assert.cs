using NUnit.Framework;
using ServiceScheduler;
using System.Text;

namespace ServiceSchedulerTests.helpers
{
    public class Assert
    {
        public static void ExecutionDateTimeMatch(int expectedHour, int expectedMinute, int expectedDay, int expectedMonth, int expectedYear, bool expectedStop, bool expectedOnce, ExecutionDateTime actual)
        {
            var sb = new StringBuilder();

            if (expectedHour != actual.ScheduledTime.Hour)
            {
                sb.AppendFormat("hour expected:{0} actual:{1}, ", expectedHour, actual.ScheduledTime.Hour);
            }

            if (expectedMinute != actual.ScheduledTime.Minute)
            {
                sb.AppendFormat("minute expected:{0} actual:{1}, ", expectedMinute, actual.ScheduledTime.Minute);
            }

            if (expectedDay != actual.ScheduledTime.Day)
            {
                sb.AppendFormat("day expected:{0} actual:{1}, ", expectedDay, actual.ScheduledTime.Day);
            }

            if (expectedMonth != actual.ScheduledTime.Month)
            {
                sb.AppendFormat("month expected:{0} actual:{1}, ", expectedMonth, actual.ScheduledTime.Month);
            }

            if (expectedYear != actual.ScheduledTime.Year)
            {
                sb.AppendFormat("year expected:{0} actual:{1}, ", expectedYear, actual.ScheduledTime.Year);
            }

            if (expectedStop != actual.IsStop)
            {
                sb.AppendFormat("isStop expected:{0} actual:{1}, ", expectedStop, actual.IsStop);
            }

            if (expectedOnce != actual.IsOnce)
            {
                sb.AppendFormat("once expected:{0} actual:{1}, ", expectedOnce, actual.IsOnce);
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, "Mismatched ");
                sb.Replace(',', '.', sb.Length - 2, 1);
                var message = sb.ToString();
                throw new AssertionException(message);
            }
        }

    }
}
