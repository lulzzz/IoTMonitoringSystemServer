using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string SensorCode { get; set; }
        public string SensorName { get; set; }
        public ICollection<Status> Statuses { get; set; }
        public ICollection<Rack> Racks { get; set; }
        public Room Room { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Log> Logs { get; set; }
        public Sensor()
        {
            Statuses = new Collection<Status>();
            Racks = new Collection<Rack>();
            Logs = new Collection<Log>();
        }
    }
}
