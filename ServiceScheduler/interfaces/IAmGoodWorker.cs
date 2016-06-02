using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceScheduler.interfaces
{
    public interface IAmGoodWorker : IDoWork, IStop, IHandleException
    {
    }
}
