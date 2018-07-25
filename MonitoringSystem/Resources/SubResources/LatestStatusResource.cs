using System;
using System.Collections.Generic;

namespace MonitoringSystem.Resources.SubResources
{
    public class LatestStatusResource
    {
        public string placement { get; set; }
        public string text { get; set; }
        public string sensor { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
        public LatestStatusResource()
        {
            placement = "top";
        }
    }
}