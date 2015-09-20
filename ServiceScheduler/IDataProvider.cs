using System;
using System.Collections.Generic;

namespace ServiceScheduler
{
	public interface IDataProvider
	{
		IList<string> GetRecurrentTimes();
		IList<string> GetOnceTimes();
	}
}

