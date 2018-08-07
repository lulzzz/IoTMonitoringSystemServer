using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Resources
{
    public class LogResource
    {
        public int LogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public LogResource()
        {
            IsDeleted = false;
        }
    }
}
