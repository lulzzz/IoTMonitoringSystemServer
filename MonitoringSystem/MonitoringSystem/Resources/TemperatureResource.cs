using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Resources
{
    public class TemperatureResource
    {
        public int TemperatureId { get; set; }
        public int Value { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTime { get; set; }
        public TemperatureResource()
        {
            IsDeleted = false;
            DateTime = DateTime.Now;
        }
    }
}
