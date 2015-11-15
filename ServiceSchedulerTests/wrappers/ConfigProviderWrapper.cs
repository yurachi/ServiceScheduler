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

		public DateTime Now 
		{
			set
			{
				_dateTimeNow = () => value;
			}	
		}
	}
}

