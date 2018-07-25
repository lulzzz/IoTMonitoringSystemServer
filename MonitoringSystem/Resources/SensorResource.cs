using MonitoringSystem.Resources.SubResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Resources
{
    public class SensorResource
    {
        public int SensorId { get; set; }
        public string SensorCode { get; set; }
        public string SensorName { get; set; }
        public ICollection<int> Statuses { get; set; }
        public ICollection<int> Racks { get; set; }
        public ICollection<string> RackNames { get; set; }
        public int? RoomId { get; set; }
        public RoomResource Room { get; set; }
        public bool IsDeleted { get; set; }
        public LatestStatusResource LatestStatus { get; set; }
        public SensorResource()
        {
            Statuses = new Collection<int>();
            Racks = new Collection<int>();
            IsDeleted = false;
            RackNames = new Collection<string>();
        }
    }
}