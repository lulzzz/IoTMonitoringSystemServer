using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Resources
{
    public class FanResource
    {
        public int FanId { get; set; }
        public string FanCode { get; set; }
        public string FanName { get; set; }
        public bool IsOn { get; set; }
        public int Capacity { get; set; }
        public bool IsDeleted { get; set; }
        public int? RoomId { get; set; }
        public RoomResource Room { get; set; }
        public ICollection<int> FanStatuses { get; set; }
        public FanResource()
        {
            FanStatuses = new Collection<int>();
            IsDeleted = false;
        }
    }
}
