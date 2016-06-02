using System;

namespace ServiceScheduler.datamodels
{
    public class DataSourceDateTime
    {
        public string ScheduledTime { get; set; }
        public string DayOfWeek { get; set; }
        public string ServiceClassName { get; set; }
        public bool IsStop { get; set; }
        public bool IsOnce { get; set; }
        public Action Remove { get; set; }
        public Func<string> AddError { get; set; }
    }
}
