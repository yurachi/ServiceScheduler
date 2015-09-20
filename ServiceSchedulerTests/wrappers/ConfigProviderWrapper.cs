using System;
using System.Collections.Generic;
using ServiceScheduler;

namespace ServiceSchedulerTests
{
	public class ConfigProviderWrapper : ConfigProvider
	{
		public ConfigProviderWrapper (IDataProvider dataProvider) : base(dataProvider)
		{
			
		}

		new public void AddTodayTimes(IList<string> recurrentTimeCollection)
		{
			base.AddTodayTimes(recurrentTimeCollection);
		}

		public DateTime Now 
		{
			set
			{
				_dateTimeNow = () => value;
			}	
		}

		public IList<DateTime> ExecutionTimes
		{
			get
			{
				return _executionTimes;
			}
		}
	}
}

