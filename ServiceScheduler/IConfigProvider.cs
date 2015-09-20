using System;

namespace ServiceScheduler
{
	public interface IConfigProvider
	{
		void Setup();
		DateTime GetNextExecutionTime();
		TimeSpan GetMinimalTimeInterval ();
		TimeSpan GetMinimalSleepInterval();
	}
}

