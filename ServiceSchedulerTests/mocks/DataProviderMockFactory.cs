using System;
using NSubstitute;
using ServiceScheduler;

namespace ServiceSchedulerTests
{
	public class DataProviderMockFactory
	{
		private IDataProvider _dataProviderMock;

		public DataProviderMockFactory()
		{
			_dataProviderMock =  Substitute.For<IDataProvider>();
		}

		public IDataProvider Create()
		{
			return _dataProviderMock;
		}
	}
}

