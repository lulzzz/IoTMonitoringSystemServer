using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Temperature
    {
        public int TemperatureId { get; set; }
        public int Value { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTime { get; set; }
        public Temperature()
        {
            IsDeleted = false;
            DateTime = DateTime.Now;
        }
    }
}
