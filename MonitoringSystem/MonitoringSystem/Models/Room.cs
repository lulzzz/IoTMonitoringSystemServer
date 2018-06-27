using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public ICollection<Status> Status { get; set; }
        public ICollection<Rack> Racks { get; set; }
        public ICollection<Fan> Fans { get; set; }
    }
}
