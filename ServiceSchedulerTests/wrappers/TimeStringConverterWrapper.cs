using System;
using System.Collections.Generic;
using ServiceScheduler;

namespace ServiceSchedulerTests
{
	public class TimeStringConverterWrapper : TimeStringConverter
	{
		new public DateTime ParseDateTime(string timeString, string dayOfWeek)
		{
			return base.ParseDateTime (timeString, dayOfWeek);
		}

        new public int GetDayForPastTime(DateTime parsedTime, string dayOfWeek)
        {
            return base.GetDayForTimeInPast(parsedTime, dayOfWeek);
        }

        new public int GetWeekDaysDifference(string dayOfWeek)
        {
            return base.GetWeekDaysDifference(dayOfWeek);
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
