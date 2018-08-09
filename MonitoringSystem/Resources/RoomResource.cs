using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Resources
{
    public class RoomResource
    {
        public int RoomId { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public ICollection<int> Sensors { get; set; }
        public ICollection<int> Racks { get; set; }
        public ICollection<int> Fans { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<LogResource> Logs { get; set; }
        public RoomResource()
        {
            Sensors = new Collection<int>();
            Racks = new Collection<int>();
            Fans = new Collection<int>();
            Logs = new Collection<LogResource>();
        }
    }
}
