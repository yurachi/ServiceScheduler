using System;

namespace ServiceScheduler
{
	public interface ITimeStringConverter
	{
        TimeSpan Tolerance { set; }
		ExecutionDateTime Convert(string timeString);
	}
}

