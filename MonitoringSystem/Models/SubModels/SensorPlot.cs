using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Models
{
    public class SensorPlot
    {
        public ICollection<string> x { get; set; }
        public ICollection<double> y { get; set; }
        public string type { get; set; }
        public SensorPlot()
        {
            type = "scatter";
            x = new Collection<string>();
            y = new Collection<double>();
        }
    }
}