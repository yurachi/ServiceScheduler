using System;
using System.Collections.Generic;
using ServiceScheduler;

namespace ServiceSchedulerTests
{
	public class ConfigProviderWrapper : ConfigProvider
	{
		public ConfigProviderWrapper (IDataProvider dataProvider, ITimeStringConverter timeStringConverter) : base(dataProvider, timeStringConverter)
		{
			
		}

        new public void InsertExecutionTimes(IEnumerable<ExecutionDateTime> newTimes)
        {
            base.InsertExecutionTimes(newTimes);
        }

        new public IEnumerable<ExecutionDateTime> CreateResetConfigTimes()
        {
            return base.CreateResetServiceAndConfigExecutionTimes();
        }

        new public void RemoveServiceExecutionTimes()
        {
            base.RemoveServiceExecutionTimes();
        }

        public List<ExecutionDateTime> ExecutionTimes
        {
            get
            {
                return _executionTimes;
            }

            set
            {
                _executionTimes = value;
            }
        }

        public DateTime Now 
		{
			set
			{
				_dateTimeNow = () => value;
			}	
		}
	}
}

