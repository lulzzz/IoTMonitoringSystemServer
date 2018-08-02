using System;

namespace MonitoringSystem.Resources
{
    public class FanStatusResource
    {
        public int FanStatusId { get; set; }
        public FanResource Fan { get; set; }
        public int FanId { get; set; }
        public string FanCode { get; set; }
        public bool IsDeleted { get; set; }
        public double Vibration { get; set; }
        public DateTime DateTime { get; set; }
        public FanStatusResource()
        {
            IsDeleted = false;
        }
    }
}
