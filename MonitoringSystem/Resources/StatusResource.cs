using System;

namespace MonitoringSystem.Resources
{
    public class StatusResource
    {
        public int StatusId { get; set; }
        public DateTime DateTime { get; set; }
        public int? TemperatureId { get; set; }
        public TemperatureResource Temperature { get; set; }
        public double? TemperatureValue { get; set; }
        public int? HumidityId { get; set; }
        public HumidityResource Humidity { get; set; }
        public double? HumidityValue { get; set; }
        public bool IsDeleted { get; set; }
        public int? SensorId { get; set; }
        public string SensorCode { get; set; }
        public SensorResource Sensor { get; set; }
    }
}
