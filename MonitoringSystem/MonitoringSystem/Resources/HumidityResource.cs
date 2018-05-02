using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Resources
{
    public class HumidityResource
    {
        public int HumidityId { get; set; }
        public int Value { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTime { get; set; }
        public HumidityResource()
        {
            IsDeleted = false;
            DateTime = DateTime.Now;
        }
    }
}
