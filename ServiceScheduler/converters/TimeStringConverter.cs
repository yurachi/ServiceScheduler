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
            Tolerance = new TimeSpan(0, 1, 0);
		}

		public ExecutionDateTime Convert(DataSourceDateTime sourceTime)
		{
			var parsedDateTime = _dateTimeNow ();
			parsedDateTime = ParseDateTime (sourceTime.ScheduledTime, sourceTime.DayOfWeek);
			var scheduled = new ExecutionDateTime () 
			{
				IsOnce = sourceTime.IsOnce,
                IsStop = sourceTime.IsStop,
				Remove = sourceTime.Callback,
				ScheduledTime = parsedDateTime,
			};
			return scheduled;
		}

		protected DateTime ParseDateTime(string timeString, string dayOfWeek)
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

        protected int GetWeekDaysDifference(string dayOfWeek)
        {
            var scheduledDayOfWeek = (int)Enum.Parse(typeof(DayOfWeek), dayOfWeek);
            var currentDayOfWeek = (int)_dateTimeNow().DayOfWeek;
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
            if ((parsedTime.TimeOfDay < _dateTimeNow().TimeOfDay) && 
                (dayOfWeek == null))
                return 1;
            else
                return 0;
        }

        public TimeSpan Tolerance { protected get; set; }
    }
}

