namespace MonitoringSystem.Models
{
    public class Temperature
    {
        public int TemperatureId { get; set; }
        public int Value { get; set; }
        public bool IsDeleted { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
