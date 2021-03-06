using System;
using System.Collections.Generic;

namespace MonitoringSystem.Models
{
    public class Plot
    {
        public ICollection<DateTime> x { get; set; }
        public ICollection<double> y { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public Plot()
        {
            type = "scatter";
        }
    }
}