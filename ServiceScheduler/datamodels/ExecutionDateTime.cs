using System;

namespace ServiceScheduler
{
	public class ExecutionDateTime
	{
		public ExecutionDateTime ()
		{
		}

		public bool IsOnce { get; set;}
		public bool IsNow { get; set;}
		public DateTime ScheduledTime { get; set;}
	}
}

