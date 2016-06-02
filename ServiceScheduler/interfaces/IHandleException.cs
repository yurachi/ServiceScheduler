using System;

namespace ServiceScheduler.interfaces
{
    public interface IHandleException
    {
        void HandleException(Exception ex);
    }
}
