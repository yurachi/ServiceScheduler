using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceScheduler.interfaces
{
    public interface IWorkerProvider
    {
        IAmGoodWorker GetWorker(string workerClassName);
    }
}
