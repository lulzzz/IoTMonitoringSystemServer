using System;
using System.Collections.Generic;

namespace MonitoringSystem.Models
{
    public class PlotResource
    {
        public ICollection<DateTime> x { get; set; }
        public ICollection<double> y { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public PlotResource()
        {
            type = "scatter";
        }
    }
}