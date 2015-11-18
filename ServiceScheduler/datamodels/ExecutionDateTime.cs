using System;

namespace ServiceScheduler
{
	public class ExecutionDateTime
	{
		public ExecutionDateTime ()
		{
		}

        public string ServiceMethodName { get; set; }
		public bool IsOnce { get; set;}
        public bool IsStop { get; set; }
		public DateTime ScheduledTime { get; set;}
        public Action Remove { get; set; }
        public Func<string> AddError { get; set; }
	}
}

