using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public DateTime DateTime { get; set; }
        public Temperature Temperature { get; set; }
        public Humidity Humidity { get; set; }
    }
}
