using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceScheduler.datamodels
{
    public class DataSourceDateTime
    {
        public string ScheduledTime { get; set; }
        public string DayOfWeek { get; set; }
        public bool IsStop { get; set; }
        public bool IsOnce { get; set; }
        public Action Callback { get; set; }
    }
}
