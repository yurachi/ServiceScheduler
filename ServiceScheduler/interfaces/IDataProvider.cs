using ServiceScheduler.datamodels;
using System;
using System.Collections.Generic;

namespace ServiceScheduler
{
	public interface IDataProvider
	{
        IList<DataSourceDateTime> GetRecurrentTimes();
	}
}

