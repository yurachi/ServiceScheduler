using System;
using System.Collections.Generic;

namespace ServiceScheduler
{
	public class ConfigProvider : IConfigProvider
	{
		protected List<ExecutionDateTime> _executionTimes;
		protected IDataProvider _dataProvider;
		protected Func<DateTime> _dateTimeNow;

		public ConfigProvider (IDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
			_executionTimes = new List<ExecutionDateTime> ();
			_dateTimeNow = () => DateTime.Now;
            InsertExecutionTimes(CreateResetConfigTimes());
		}

        protected void InsertExecutionTimes(IEnumerable<ExecutionDateTime> newTimes)
        {
            throw new NotImplementedException();
        }

        //22:00 till 08:00  every hour = 9 runs (exclusive)
        //08:00 till 22:00 every 5 min = 168 runs (inclusive)
        protected IEnumerable<ExecutionDateTime> CreateResetConfigTimes()
        {
            var smallStep = new TimeSpan(0, 5, 0);
            var configTimes = new List<ExecutionDateTime>();
            var offset = new TimeSpan();
            for(var i = 0; i < 177; ++i)
            {
                var time = _dateTimeNow().Add(offset);
                if (time.TimeOfDay.TotalMinutes < 421 || time.TimeOfDay.TotalMinutes > 1319)
                {
                    offset.Add(new TimeSpan(1,0,0));
                }
                else
                {
                    offset.Add(new TimeSpan(0, 5, 0));
                }
                var executionDateTime = new ExecutionDateTime()
                {
                    IsOnce = false,
                    IsStop = false,
                    ScheduledTime = time,
                    ServiceMethodName = "ConfigProvider.ResetServiceExecutionTimes",
                };
                configTimes.Add(executionDateTime);
            }
            return configTimes;
        }

        protected void ResetServiceExecutionTimes(IEnumerable<ExecutionDateTime> newTimes)
		{
            RemoveServiceExecutionTimes();
            var serviceExecutionTimes = LoadServiceExecutionTimes();
            InsertExecutionTimes(serviceExecutionTimes);
		}

        protected void RemoveServiceExecutionTimes()
        {
            throw new NotImplementedException();
        }

        protected IEnumerable<ExecutionDateTime> LoadServiceExecutionTimes()
        {
            throw new NotImplementedException();
        }

        public ExecutionDateTime GetNextExecutionTime()
		{
            //TODO: wait if Pause
            //TODO: find nearest execution time in the collection
            //TODO: in the case of conflict ConfigReread is low priority
            throw new NotImplementedException();
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

        public void ResetConfig()
        {
            throw new NotImplementedException();
        }
    }
}

