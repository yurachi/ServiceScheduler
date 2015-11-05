using System;
using System.Linq;
using System.Globalization;

namespace ServiceScheduler
{
	public class TimeStringConverter : ITimeStringConverter
	{
		protected Func<DateTime> _dateTimeNow;

		public TimeStringConverter()
		{
			_dateTimeNow = () => DateTime.Now;
            Tolerance = new TimeSpan(0, 1, 0);
		}

		public ExecutionDateTime Convert(string timeString)
		{
			var timeTokens = timeString.Split (' ');
			var recognisedCount = 0;

			var isOnce = timeTokens.Last ().Equals ("once");
			if (isOnce)
				++recognisedCount;
			
			var isNow = timeTokens.First ().Equals ("now");
			if (isNow)
				++recognisedCount;

            if (isNow)
                isOnce = true;

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
				ScheduledTime = isNow ? parsedDateTime.Add(Tolerance) : parsedDateTime.Date,
			};
			return date;
		}

		protected DateTime ParseDateTime(params string[] timeStrings)
		{
            DateTime result;

            var parsedTime = DateTime.Parse(timeStrings[0], CultureInfo.CreateSpecificCulture("en-UK"), DateTimeStyles.NoCurrentDateDefault);
                result = _dateTimeNow().Date
                    .Add(parsedTime.TimeOfDay)
                    .AddDays((parsedTime.TimeOfDay.Add(Tolerance) > _dateTimeNow().TimeOfDay) ? 0 : 1);

            var dayString = string.Empty;
                if (timeStrings[1].Length < 6)
                    yearString = "-" + _dateTimeNow().Year.ToString();
                result = DateTime.Parse(timeStrings[1] + yearString + " " + timeStrings[0], CultureInfo.CreateSpecificCulture("en-UK"));
            
			return result;
        }

        public TimeSpan Tolerance { protected get; set; }
    }
}

