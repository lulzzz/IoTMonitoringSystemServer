using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Models
{
    public class Humidity
    {
        public int HumidityId { get; set; }
        public int Value { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTime { get; set; }
        public Humidity()
        {
            IsDeleted = false;
            DateTime = DateTime.Now;
        }
    }
}
