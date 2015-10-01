using System;
using System.Collections.Generic;
using ServiceScheduler;

namespace ServiceSchedulerTests
{
	public class TimeStringConverterWrapper : TimeStringConverter
	{
		new public DateTime ParseDateTime(params string[] timeString)
		{
			return base.ParseDateTime (timeString);
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
