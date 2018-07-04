using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Resources
{
    public class RackResource
    {
        public int RackId { get; set; }
        public string RackCode { get; set; }
        public string RackName { get; set; }
        public ICollection<int> Statuses { get; set; }
        public bool IsDeleted { get; set; }
        public int? RoomId { get; set; }
        public RoomResource Room { get; set; }
        public RackResource()
        {
            Statuses = new Collection<int>();
        }
    }
}
