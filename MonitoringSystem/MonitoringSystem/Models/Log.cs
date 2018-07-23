using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public Log()
        {
            IsDeleted = false;
        }
    }
}
