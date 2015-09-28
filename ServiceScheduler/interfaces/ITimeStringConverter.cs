using System;

namespace ServiceScheduler
{
	public interface ITimeStringConverter
	{
		ExecutionDateTime Convert(string timeString, int addedDays, TimeSpan tolerance);
	}
}

