using System;

namespace ServiceScheduler
{
	public class ConfigProvider
	{
		protected IList<DateTime> _executionTimes;

		public ConfigProvider ()
		{
		}

		public void Setup()
		{
			Pause ();
			_executionTimes.Clean ();
			var recurrentTimes = _dataProvider.GetRecurrentTimes ();
			AddTodayTimes (recurrentTimes);
			AddTomorrowTimes (recurrentTimes);
			AddOnceTimes (_dataProvider.GetOnceTimes ());
			Resume ();
		}

		protected void AddTodayTimes(IList<string> recurrentTimeCollection)
		{
		}

		protected void AddTomorrowTimes(IList<string> recurrentTimeCollection)
		{
		}

		protected void AddOnceTimes(IList<string> timeCollection)
		{
			
		}

		public DateTime GetNextExecutionTime()
		{
			//TODO: wait if Pause
			//TODO: find nearest execution time in the collection
		}

		public Interval GetMinimalTimerInterval()
		{
			//TODO: wait if Pause

		}

		public Interval GetMinimalSleepInterval()
		{
			//TODO: wait if Pause
			//TODO: return minimal of (NextExecution - Now) and DefaultSleepInterval
		}
	}
}

