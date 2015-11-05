using System;

namespace ServiceScheduler
{
	public interface IConfigProvider
	{
		void Setup();
		ExecutionDateTime GetNextExecutionTime();
		TimeSpan GetMinimalTimeInterval ();
		TimeSpan GetMinimalSleepInterval();
	}
}

