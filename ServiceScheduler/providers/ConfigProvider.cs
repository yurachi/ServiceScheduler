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

        protected IEnumerable<ExecutionDateTime> CreateResetConfigTimes()
        {
            //00:58 till 07:58  every hour
            //08:03 till 21:58 every 5 min
            throw new NotImplementedException();
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

