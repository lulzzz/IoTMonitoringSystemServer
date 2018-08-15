using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Resources
{
    public class SensorPlotResource
    {
        public ICollection<string> x { get; set; }
        public ICollection<double> y { get; set; }
        public string type { get; set; }
        public SensorPlotResource()
        {
            type = "scatter";
            x = new Collection<string>();
            y = new Collection<double>();
        }
    }
}