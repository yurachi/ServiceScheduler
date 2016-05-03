using System;
using System.Collections.Generic;

namespace ServiceScheduler
{
	public interface IConfigProvider
	{
		ExecutionDateTime GetNextExecutionTime();
		TimeSpan GetMinimalTimeInterval ();
		TimeSpan GetMinimalSleepInterval();
        void ResetServiceExecutionTimes();
        void ResetAllExecutionTimes();
	}
}

