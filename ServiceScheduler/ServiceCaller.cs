using System;
using System.Threading;

namespace ServiceScheduler
{
	public class ServiceCaller
	{
		protected Action _doWork;
		protected IConfigProvider _configProvider;
        protected Func<DateTime> _dateTimeNow;
        protected Action<int> _threadSleep;

        public ServiceCaller (IConfigProvider configProvider)
		{
			_configProvider = configProvider;
            _dateTimeNow = () => DateTime.Now;
            _threadSleep = (x) => Thread.Sleep(x);
        }

        public void MainLoop(Action doWork)
		{
			_doWork = doWork;
			while(true)
			{
                var nextExecutionTime = _configProvider.GetNextExecutionTime();
                if (TimeDifference(nextExecutionTime.ScheduledTime, _dateTimeNow) < _configProvider.GetMinimalTimeInterval ()) 
				{
                    if (nextExecutionTime.IsOnce)
                        nextExecutionTime.Remove();
					_doWork ();//TODO:background thread?
				}
				else
				{
					_threadSleep(_configProvider.GetMinimalSleepInterval ().Milliseconds);
				}
			}
		}
	}
}

