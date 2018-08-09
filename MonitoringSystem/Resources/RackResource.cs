using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Resources
{
    public class RackResource
    {
        public int RackId { get; set; }
        public string RackCode { get; set; }
        public string RackName { get; set; }
        public int? SensorId { get; set; }
        public SensorResource Sensor { get; set; }
        public bool IsDeleted { get; set; }
        public int? RoomId { get; set; }
        public string RoomName { get; set; }
        public int Location { get; set; }
        public RoomResource Room { get; set; }
        public ICollection<LogResource> Logs { get; set; }
        public RackResource()
        {
            IsDeleted = false;
            Logs = new Collection<LogResource>();
        }
    }
}
