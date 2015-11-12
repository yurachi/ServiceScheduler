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
            MainLoopRunning = true;
        }

        public void MainLoop(Action doWork)
		{
			_doWork = doWork;
            ExecutionDateTime nextExecutionTime;
            while (MainLoopRunning)
			{
                nextExecutionTime = _configProvider.GetNextExecutionTime();
                if (CalculateAbsoluteTimeDifference(nextExecutionTime.ScheduledTime) < _configProvider.GetMinimalTimeInterval ()) 
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

        protected TimeSpan CalculateAbsoluteTimeDifference(DateTime scheduledTime)
        {
            if (scheduledTime > _dateTimeNow())
                return scheduledTime.Subtract(_dateTimeNow());
            else
                return _dateTimeNow().Subtract(scheduledTime);
        }

        public virtual bool MainLoopRunning { get; set; } //TODO: make it Synchronised<bool>
    }
}

