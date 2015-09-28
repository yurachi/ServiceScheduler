using System;
using System.Linq;

namespace ServiceScheduler
{
	public class TimeStringConverter : ITimeStringConverter
	{
		protected Func<DateTime> _dateTimeNow;

		public TimeStringConverter()
		{
			_dateTimeNow = () => DateTime.Now;
		}

		public ExecutionDateTime Convert(string timeString, int addedDays, TimeSpan tolerance)
		{
			var timeTokens = timeString.Split (' ');
			var recognisedCount = 0;

			var isOnce = timeTokens.Last ().Equals ("once");
			if (isOnce)
				++recognisedCount;
			
			var isNow = timeTokens.First ().Equals ("now");
			if (isNow)
				++recognisedCount;

			var parsedDateTime = _dateTimeNow ();
			if(!isNow)
			{
				if (!isOnce)
				{
					parsedDateTime = ParseDateTime (timeTokens);
				}
				else
				{
					if (timeTokens.Length == 2)
					{
						parsedDateTime = ParseDateTime (timeTokens [0]);
						++recognisedCount;
					}
					else
					{
						parsedDateTime = ParseDateTime (timeTokens [0],timeTokens [1]);
						recognisedCount += 2;
					}
				}
			}
			//TODO: check recognisedCount
			var date = new ExecutionDateTime () 
			{
				IsOnce = isOnce,
				IsNow = isNow,
				ScheduledTime = isNow ? parsedDateTime.AddDays (addedDays).Add(tolerance) : parsedDateTime.Date.AddDays (addedDays),
			};
			return date;
		}

		protected DateTime ParseDateTime(params string[] timeString)
		{
			return _dateTimeNow ();
		}
	}
}

