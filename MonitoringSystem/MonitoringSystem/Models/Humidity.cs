namespace MonitoringSystem.Models
{
    public class Humidity
    {
        public int HumidityId { get; set; }
        public double Value { get; set; }
        public bool IsDeleted { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
