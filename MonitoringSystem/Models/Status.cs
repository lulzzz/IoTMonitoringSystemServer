using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public DateTime DateTime { get; set; }
        public Temperature Temperature { get; set; }
        public Humidity Humidity { get; set; }
        public bool IsDeleted { get; set; }
        public Sensor Sensor { get; set; }
        public ICollection<Log> Logs { get; set; }
        public Status()
        {
            IsDeleted = false;
            Logs = new Collection<Log>();
        }
    }
}
