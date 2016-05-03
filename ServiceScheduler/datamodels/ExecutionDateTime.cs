using System;

namespace ServiceScheduler
{
	public class ExecutionDateTime
	{
		public ExecutionDateTime ()
		{
		}

        public string ServiceName { get; set; }
		public bool IsOnce { get; set;}
        public bool IsStop { get; set; }
        public bool IsWholeDay { get; set; }
		public DateTime ScheduledTime { get; set;}
        public Action Remove { get; set; }
        public Func<string> AddError { get; set; }
	}
}

