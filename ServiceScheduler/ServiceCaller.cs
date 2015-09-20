using System;
using System.Threading;

namespace ServiceScheduler
{
	public class ServiceCaller
	{
		protected Action _doWork;
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
				if ((_configProvider.GetNextExecutionTime () - DateTime.Now) < _configProvider.GetMinimalTimeInterval ()) 
				{
					_doWork ();//TODO:background thread?
				}
				else
				{
					Thread.Sleep(_configProvider.GetMinimalSleepInterval ().Milliseconds);
				}
			}
		}
	}
}

