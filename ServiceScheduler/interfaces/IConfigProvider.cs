using System;

namespace ServiceScheduler.interfaces
{
    public interface IConfigProvider
	{
		ExecutionDateTime GetNextExecutionTime();
		TimeSpan GetMinimalTimeInterval ();
		TimeSpan GetMinimalSleepInterval();
        void ResetServiceExecutionTimes();
        void ResetConfigExecutionTimes();
	}
}

