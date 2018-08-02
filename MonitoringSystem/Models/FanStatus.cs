using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Models
{
    public class FanStatus
    {
        public int FanStatusId { get; set; }
        public Fan Fan { get; set; }
        public DateTime DateTime { get; set; }
        public double Vibration { get; set; }
        public ICollection<Log> Logs { get; set; }
        public bool IsDeleted { get; set; }
        public FanStatus()
        {
            IsDeleted = false;
            Logs = new Collection<Log>();
        }
    }
}