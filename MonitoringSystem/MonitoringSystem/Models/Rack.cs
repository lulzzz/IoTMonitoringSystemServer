using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Models
{
    public class Rack
    {
        public int RackId { get; set; }
        public string RackCode { get; set; }
        public string RackName { get; set; }
        public ICollection<Status> Statuses { get; set; }
        public Room Room { get; set; }
        public bool IsDeleted { get; set; }
        public Rack()
        {
            Statuses = new Collection<Status>();
        }
    }
}
