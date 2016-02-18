using System;
using System.Linq;
using System.Globalization;
using ServiceScheduler.datamodels;

namespace ServiceScheduler
{
	public class TimeStringConverter : ITimeStringConverter
	{
		protected Func<DateTime> _dateTimeNow;

		public TimeStringConverter()
		{
			_dateTimeNow = () => DateTime.Now;
            Tolerance = new TimeSpan(0, 0, 59);
		}

		public ExecutionDateTime Convert(DataSourceDateTime sourceTime)
		{
			var parsedDateTime = _dateTimeNow ();
			parsedDateTime = ParseTime (sourceTime.ScheduledTime, sourceTime.DayOfWeek);
			var scheduled = new ExecutionDateTime () 
			{
				IsOnce = sourceTime.IsOnce,
                IsStop = sourceTime.IsStop,
				Remove = sourceTime.Remove,
				ScheduledTime = parsedDateTime,
			};
			return scheduled;
		}

		protected DateTime ParseTime(string timeString, string dayOfWeek)
		{
            DateTime result;

            var parsedTime = DateTime.Parse(
                timeString, 
                CultureInfo.CreateSpecificCulture("en-UK"), 
                DateTimeStyles.NoCurrentDateDefault);

            result = _dateTimeNow().Date
                .Add(parsedTime.TimeOfDay)
                .AddDays(GetDayForTimeInPast(parsedTime, dayOfWeek))
                .AddDays(GetWeekDaysDifference(dayOfWeek));
           
			return result;
        }

        /// <summary>
        /// If the specified day of week is passed, schedule for the next week
        /// </summary>
        protected int GetWeekDaysDifference(string dayOfWeek)
        {
            var currentDayOfWeek = (int)_dateTimeNow().DayOfWeek;
            var scheduledDayOfWeek = (int)Enum.Parse(typeof(DayOfWeek), dayOfWeek??_dateTimeNow().DayOfWeek.ToString());
            var dayDifference = scheduledDayOfWeek - currentDayOfWeek;
            if (dayDifference < 0)
                dayDifference += 7;
            return dayDifference;
        }

        /// <summary>
        /// If the time is passed today, add 1 day for scheduling on tomorrow, 
        /// unless the weekday is specified
        /// </summary>
        protected int GetDayForTimeInPast(DateTime parsedTime, string dayOfWeek)
        {
            if ((parsedTime.TimeOfDay.Add(Tolerance)< _dateTimeNow().TimeOfDay) && 
                (dayOfWeek == null))
                return 1;
            else
                return 0;
        }

        public TimeSpan Tolerance { protected get; set; }
    }
}

