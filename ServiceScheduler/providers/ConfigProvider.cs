using System;
using System.Collections.Generic;

namespace ServiceScheduler
{
	public class ConfigProvider : IConfigProvider
	{
		protected List<DateTime> _executionTimes;
		protected List<string> _recurrentTimes;
		protected List<string> _onceTimes;
		protected IDataProvider _dataProvider;
		protected Func<DateTime> _dateTimeNow;

		public ConfigProvider (IDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
			_executionTimes = new List<DateTime> ();
			_recurrentTimes = new List<string> ();
			_onceTimes = new List<string> ();
			_dateTimeNow = () => DateTime.Now;
		}

		public void Setup()
		{
			Pause ();
			_executionTimes.Clear ();
			_recurrentTimes.Clear ();
			//_recurrentTimes.AddRange(_dataProvider.GetRecurrentTimes ());
			AddTodayTimes (_recurrentTimes);
			AddTomorrowTimes (_recurrentTimes);
			_onceTimes.Clear ();
			//_onceTimes.AddRange(_dataProvider.GetOnceTimes ());
			AddOnceTimes (_onceTimes);
			Resume ();
		}

		protected void Pause()
		{
		}

		protected void Resume()
		{
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

		public ExecutionDateTime GetNextExecutionTime()
		{
			//TODO: wait if Pause
			//TODO: find nearest execution time in the collection
			return new ExecutionDateTime();
		}

		public TimeSpan GetMinimalTimeInterval()
		{
			//TODO: wait if Pause
			return new TimeSpan(0,1,0);
		}

		public TimeSpan GetMinimalSleepInterval()
		{
			//TODO: wait if Pause
			//TODO: return minimal of (NextExecution - Now) and DefaultSleepInterval
			return new TimeSpan(0,0,59); //59 seconds
		}
	}
}

