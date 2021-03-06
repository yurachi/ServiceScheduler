﻿using System;
using System.Text.RegularExpressions;
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
            InsertExecutionTimes(CreateResetServiceAndConfigExecutionTimes());
		}

        protected void InsertExecutionTimes(IEnumerable<ExecutionDateTime> newTimes)
        {
            foreach (var newTime in newTimes)
			{
                if(newTime.IsStop && newTime.IsWholeDay)
                {
                    newTime.ScheduledTime = newTime.ScheduledTime.Date;
                    RemoveServiceExecutionTimes(newTime);
                }
                var index = 0;
                while (index < _executionTimes.Count && (_executionTimes[index]).ScheduledTime < newTime.ScheduledTime)
                {
                    ++index;
                }
                if (index < _executionTimes.Count)
                {
                    if (_executionTimes[index].ServiceName != newTime.ServiceName)
                    {
                        _executionTimes.Insert(index, newTime);
                    }
                    else if ((newTime.IsStop) && (newTime.ScheduledTime == _executionTimes[index].ScheduledTime))
                    {
                        _executionTimes.RemoveAt(index);
                        _executionTimes.Insert(index, newTime);
                    }
                }
                else
                {
                    _executionTimes.Add(newTime);
                }
			}
        }

        //22:00 till 08:00  every hour = 9 runs (excluding limits)
        //08:00 till 22:00 every 5 min = 168 runs (including limits)
        protected IEnumerable<ExecutionDateTime> CreateResetServiceAndConfigExecutionTimes()
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
                    IsWholeDay = false,
                    ScheduledTime = time,
                    ServiceName = "ConfigProvider.ResetServiceExecutionTimes",
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
            var nextCreateConfigTimesExecutionDateTime = new ExecutionDateTime()
            {
                IsOnce = false,
                IsStop = false,
                IsWholeDay = false,
                ScheduledTime = _dateTimeNow().Add(offset),
                ServiceName = "ConfigProvider.ResetConfigExecutionTimes",
            };
            return configTimes;
        }

        protected void RemoveServiceExecutionTimes()
        {
            var configRegex = new Regex("ConfigProvider.Reset(Config|Service|All)ExecutionTimes");
			_executionTimes.RemoveAll(x => !configRegex.IsMatch(x.ServiceName));
        }

        protected void RemoveServiceExecutionTimes(ExecutionDateTime wholeDayStopper)
        {
            _executionTimes.RemoveAll(
                x => x.ServiceName.Equals(wholeDayStopper.ServiceName)&&
                     x.ScheduledTime.Date.Equals(wholeDayStopper.ScheduledTime.Date));
        }

        protected void RemoveConfigExecutionTimes()
        {
            var configRegex = new Regex("ConfigProvider.Reset(Config|Service|All)ExecutionTimes");
            _executionTimes.RemoveAll(x => configRegex.IsMatch(x.ServiceName));
        }

        protected IEnumerable<ExecutionDateTime> LoadServiceExecutionTimes()
        {
			var dataSourceTimes = _dataProvider.GetRecurrentTimes();
			var executionTimes = new List<ExecutionDateTime>();
			foreach (var dataSourceTime in dataSourceTimes)
			{
				executionTimes.Add (_timeStringConverter.Convert (dataSourceTime));
			}
			return executionTimes;
        }

        public ExecutionDateTime GetNextExecutionTime()
		{
            //TODO: wait if Pause
            var nextExecutionTime = _executionTimes.Find(x => x.ScheduledTime >= _dateTimeNow());
            //TODO: in the case of conflict ConfigReread is low priority and should be moved back
            return nextExecutionTime;
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

        public void ResetServiceExecutionTimes()
        {
            var serviceExecutionTimes = LoadServiceExecutionTimes();
            RemoveServiceExecutionTimes();
            InsertExecutionTimes(serviceExecutionTimes);
        }

        public void ResetConfigExecutionTimes()
        {
            var configExecutionTimes = LoadServiceExecutionTimes();
            RemoveConfigExecutionTimes();
            InsertExecutionTimes(configExecutionTimes);
        }

        public void ResetAllExecutionTimes()
        {
            var resetServiceAndConfigExecutionTimes = CreateResetServiceAndConfigExecutionTimes();
            _executionTimes.Clear();
            _executionTimes.AddRange(resetServiceAndConfigExecutionTimes);
        }
    }
}

