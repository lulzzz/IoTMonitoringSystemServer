using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
        public ICollection<Rack> Racks { get; set; }
        public ICollection<Fan> Fans { get; set; }
        public bool IsDeleted { get; set; }
        public Room()
        {
            Sensors = new Collection<Sensor>();
            Racks = new Collection<Rack>();
            Fans = new Collection<Fan>();
        }
    }
}
