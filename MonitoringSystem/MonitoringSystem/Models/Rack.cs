using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Rack
    {
        public int RackId { get; set; }
        public int RackCode { get; set; }
        public string RackName { get; set; }
        public ICollection<Status> Status { get; set; }
    }
}
