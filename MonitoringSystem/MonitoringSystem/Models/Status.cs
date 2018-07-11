using System;

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
    }
}
