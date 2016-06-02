﻿using ServiceScheduler.interfaces;
using System.Collections.Generic;

namespace ServiceScheduler.providers
{
    public class WorkerProvider : IWorkerProvider
    {
        protected Dictionary<string, IAmGoodWorker> _workers;

        public WorkerProvider()
        {
            _workers = new Dictionary<string, IAmGoodWorker>();
        }

        public IAmGoodWorker GetWorker(string workerClassName)
        {
            if (_workers.ContainsKey(workerClassName))
            {
                return _workers[workerClassName];
            }
            else
            {
                return null;
            }
        }
    }
}
