namespace MonitoringSystem.Resources
{
    public class TemperatureResource
    {
        public int TemperatureId { get; set; }
        public int Value { get; set; }
        public bool IsDeleted { get; set; }
        public int? StatusId { get; set; }
        public StatusResource Status { get; set; }
    }
}
