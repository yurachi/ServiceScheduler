using System;

namespace ServiceScheduler
{
	public class ServiceCaller
	{
		protected Action _doWork;
		protected Timer _timer;
		protected IConfigProvider _configProvider;

		public ServiceCaller (IConfigProvider configProvider)
		{
			_configProvider = configProvider;
		}

		public void MainLoop(Action doWork)
		{
			_doWork = doWork;
			while(true)
			{
				if ((_configProvider.GetNextExecutionTime () - DateTime.Now) < _configProvider.GetMinimalTimerInterval ()) 
				{
					_doWork ();//TODO:background thread?
				}
				else
				{
					_timer.Sleep (_configProvider.GetMinimalSleepInterval ());
				}
			}
		}
	}
}

