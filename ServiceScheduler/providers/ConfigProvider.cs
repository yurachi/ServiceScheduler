using System;
using System.Collections.Generic;

namespace ServiceScheduler
{
	public class ConfigProvider : IConfigProvider
	{
		protected List<ExecutionDateTime> _executionTimes;
		protected IDataProvider _dataProvider;
		protected ITimeStringConverter _timeStringConverter;
		protected Func<DateTime> _dateTimeNow;

		public ConfigProvider (IDataProvider dataProvider, ITimeStringConverter timeStringConverter)
		{
			_dataProvider = dataProvider;
			_timeStringConverter = timeStringConverter;
			_executionTimes = new List<ExecutionDateTime> ();
			_dateTimeNow = () => DateTime.Now;
            InsertExecutionTimes(CreateResetConfigTimes());
		}

        protected void InsertExecutionTimes(IEnumerable<ExecutionDateTime> newTimes)
        {
			foreach (var time in newTimes)
			{
				var index = 0;
				while (_executionTimes[index].ScheduledTime < time.ScheduledTime && index < _executionTimes.Count)
				{
					++index;
				}
				if (index < _executionTimes.Count) {
					_executionTimes.Insert (index, time);
				} else 
				{
					_executionTimes.Add (time);
				}
			}
        }

        //22:00 till 08:00  every hour = 9 runs (excluding limits)
        //08:00 till 22:00 every 5 min = 168 runs (including limits)
        protected IEnumerable<ExecutionDateTime> CreateResetConfigTimes()
        {
            var configTimes = new List<ExecutionDateTime>();
			var offset = new TimeSpan(0, (3 - _dateTimeNow().Minute % 5) + 1, 0);  // linked to current Datetime, but the minutes are 04, 09, 14....
            for(var i = 0; i < 177; ++i)
            {
                var time = _dateTimeNow().Add(offset);
                var executionDateTime = new ExecutionDateTime()
                {
                    IsOnce = false,
                    IsStop = false,
                    ScheduledTime = time,
                    ServiceMethodName = "ConfigProvider.ResetServiceExecutionTimes",
                };

                configTimes.Add(executionDateTime);

				if (time.TimeOfDay.TotalMinutes < 421 || time.TimeOfDay.TotalMinutes > 1319)
				{
					offset = offset.Add(new TimeSpan(1,0,0));
				}
				else
				{
					offset = offset.Add(new TimeSpan(0, 5, 0));
				}
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
			_executionTimes.RemoveAll(x => x.ServiceMethodName == "ConfigProvider.ResetServiceExecutionTimes");
        }

        protected IEnumerable<ExecutionDateTime> LoadServiceExecutionTimes()
        {
			var dataSourceTimes = _dataProvider.GetRecurrentTimes();
			var executionTimes = new List<ExecutionDateTime>();
			foreach (var dataSourceTime in dataSourceTimes)
			{
				executionTimes.Add (TimeStringConverter.Convert (dataSourceTime));
			}
			return executionTimes;
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

